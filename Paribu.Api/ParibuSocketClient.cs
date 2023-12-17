using Paribu.Api.Models.SocketApi;

namespace Paribu.Api;

public partial class ParibuSocketClient : WebSocketApiClient
{
    #region Constructor/Destructor
    public ParibuSocketClient() : this(ParibuSocketClientOptions.Default)
    {
    }

    public ParibuSocketClient(ParibuSocketClientOptions options) : this(null, options)
    {
    }

    public ParibuSocketClient(ILogger logger, ParibuSocketClientOptions options) : base(logger, options)
    {
        AddGenericHandler("Welcome", WelcomeHandler);
    }
    #endregion

    #region Overrided Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthenticationProvider(credentials);
    #endregion

    #region Protected Methods
    protected virtual void WelcomeHandler(WebSocketMessageEvent messageEvent)
    {
        if (messageEvent.JsonData["event"] != null && (string)messageEvent.JsonData["event"] == "pusher:connection_established")
            return;
    }

    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;
        return true;
    }

    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null;

        // Check for Success
        if (request is ParibuStreamRequest<ParibuSocketSubscribeRequest> socRequest)
        {
            if (message["event"] != null && message["channel"] != null)
            {
                if (socRequest.Data.Channel == (string)message["channel"] && (string)message["event"] == "pusher_internal:subscription_succeeded")
                {
                    callResult = new CallResult<object>(true);
                    return true;
                }
                else
                {
                    callResult = null;
                    return false;
                }
            }
        }

        return true;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken data, object request)
    {
        if (request is ParibuStreamRequest<ParibuSocketSubscribeRequest> socRequest)
        {
            if (data["event"] == null || data["channel"] == null)
                return false;

            // Get Event
            var evt = (string)data["event"];
            var channel = (string)data["channel"];

            // Tickers
            // Market Data
            if (evt == "diff" && socRequest.Data.Channel == channel)
                return true;
        }

        return false;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
    {
        return true;
    }

    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        if (subscription == null || subscription.Request == null)
            return false;

        var request = new ParibuStreamRequest<ParibuSocketSubscribeRequest> { Event = "pusher:unsubscribe", Data = new ParibuSocketSubscribeRequest { Auth = "", Channel = ((ParibuStreamRequest<ParibuSocketSubscribeRequest>)subscription.Request).Data.Channel } };
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            return true;
        });

        return false;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection s)
    {
        throw new NotImplementedException();
    }
    #endregion

    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<ParibuTicker> onTickerData, CancellationToken ct = default)
    {
        var restcli = new ParibuRestClient();
        var tickers = await restcli.GetTickersAsync();
        var tickersDict = tickers.Success ? tickers.Data : [];

        var internalHandler = new Action<WebSocketDataEvent<ParibuStreamResponse>>(data =>
        {
            var json = JsonConvert.DeserializeObject<SocketPayload<SocketTickers>>(data.Data.Data);
            foreach (var ticker in json.Payload.Data)
            {
                var row = tickersDict.ContainsKey(ticker.Key) ? tickersDict[ticker.Key] : null;
                if (row == null) continue;

                if(ticker.Value.Lowest.HasValue) row.Lowest = ticker.Value.Lowest.Value;
                if(ticker.Value.Highest.HasValue) row.Highest = ticker.Value.Highest.Value;
                if(ticker.Value.First.HasValue) row.First = ticker.Value.First.Value;
                if(ticker.Value.Last.HasValue) row.Last = ticker.Value.Last.Value;
                if(ticker.Value.Volume.HasValue) row.Volume = ticker.Value.Volume.Value;
                if(ticker.Value.QuoteVolume.HasValue) row.QuoteVolume = ticker.Value.QuoteVolume.Value;
                if(ticker.Value.Change.HasValue) row.Change = ticker.Value.Change.Value;
                if(ticker.Value.Percentage.HasValue) row.Percentage = ticker.Value.Percentage.Value;
                if(ticker.Value.Percentage1H.HasValue) row.Percentage1H = ticker.Value.Percentage1H.Value;
                if(ticker.Value.Percentage4H.HasValue) row.Percentage4H = ticker.Value.Percentage4H.Value;
                if(ticker.Value.Average.HasValue) row.Average = ticker.Value.Average.Value;

                onTickerData(row);
            }
        });

        var request = new ParibuStreamRequest<ParibuSocketSubscribeRequest> { Event = "pusher:subscribe", Data = new ParibuSocketSubscribeRequest { Auth = "", Channel = "ticker" } };
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /*
    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarketDataAsync(string symbol, Action<ParibuStreamOrderBook> onOrderBookData, Action<ParibuStreamTrade> onTradeData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<ParibuStreamResponse>>(data =>
        {
            var patch = JsonConvert.DeserializeObject<ParibuStreamPatch<object>>(data.Data.Data);
            if (patch.Index == "orderBook")
            {
                var pob = new ParibuStreamOrderBook { Symbol = symbol };
                var json = JsonConvert.DeserializeObject<ParibuStreamPatch<ParibuStreamMerge<StreamOrderBook>>>(data.Data.Data.Replace(",\"merge\":[],", ",\"merge\":{},"));

                if (json.Patch.Merge.Asks != null && json.Patch.Merge.Asks.Data != null && json.Patch.Merge.Asks.Data.Count > 0)
                    foreach (var ask in json.Patch.Merge.Asks.Data)
                        pob.AsksToAdd.Add(new ParibuStreamOrderBookEntry { Price = ask.Key, Amount = ask.Value });

                if (json.Patch.Merge.Bids != null && json.Patch.Merge.Bids.Data != null && json.Patch.Merge.Bids.Data.Count > 0)
                    foreach (var bid in json.Patch.Merge.Bids.Data)
                        pob.BidsToAdd.Add(new ParibuStreamOrderBookEntry { Price = bid.Key, Amount = bid.Value });

                if (json.Patch.Unset != null && json.Patch.Unset.Count() > 0)
                    foreach (var unset in json.Patch.Unset)
                    {
                        var unsetrow = unset.Split('/');
                        if (unsetrow.Length == 2)
                        {
                            if (unsetrow[0] == "buy")
                                pob.BidsToRemove.Add(new ParibuStreamOrderBookEntry { Price = unsetrow[1].ToDecimal(), Amount = 0.0m });
                            if (unsetrow[0] == "sell")
                                pob.AsksToRemove.Add(new ParibuStreamOrderBookEntry { Price = unsetrow[1].ToDecimal(), Amount = 0.0m });
                        }
                    }

                onOrderBookData(pob);
            }
            else if (patch.Index == "marketMatches")
            {
                var json = JsonConvert.DeserializeObject<ParibuStreamPatch<ParibuStreamMerge<IEnumerable<ParibuStreamTrade>>>>(data.Data.Data);
                foreach (var trade in json.Patch.Merge)
                {
                    trade.Symbol = symbol;
                    onTradeData(trade);
                }
            }
        });

        var request = new ParibuStreamRequest<ParibuSocketSubscribeRequest> { Event = "pusher:subscribe", Data = new ParibuSocketSubscribeRequest { Auth = "", Channel = "prb-market-" + symbol.ToLower() } };
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
    */
}