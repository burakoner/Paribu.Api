using Paribu.Api.Models;
using Paribu.Api.Models.StreamApi;

namespace Paribu.Api;

public partial class ParibuStreamClient : StreamApiClient
{
    #region Constructor/Destructor
    public ParibuStreamClient() : this(ParibuStreamClientOptions.Default)
    {
    }

    public ParibuStreamClient(ParibuStreamClientOptions options) : base("Paribu Stream Api", options)
    {
        AddGenericHandler("Welcome", WelcomeHandler);
    }
    #endregion

    #region Overrided Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthenticationProvider(credentials);
    #endregion

    #region Common Methods
    public static void SetDefaultOptions(ParibuStreamClientOptions options)
    {
        ParibuStreamClientOptions.Default = options;
    }

    public virtual void SetApiCredentials(string apikey, string secret)
    {
        SetApiCredentials(new ApiCredentials(apikey, secret));
    }
    #endregion

    #region Protected Methods
    protected virtual void WelcomeHandler(StreamMessageEvent messageEvent)
    {
        if (messageEvent.JsonData["event"] != null && (string)messageEvent.JsonData["event"] == "pusher:connection_established")
            return;
    }

    protected override bool HandleQueryResponse<T>(StreamConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;
        return true;
    }

    protected override bool HandleSubscriptionResponse(StreamConnection connection, StreamSubscription subscription, object request, JToken message, out CallResult<object> callResult)
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

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken data, object request)
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
            if (evt == "state-updated" && socRequest.Data.Channel == channel)
                return true;
        }

        return false;
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
    {
        return true;
    }

    protected override async Task<bool> UnsubscribeAsync(StreamConnection connection, StreamSubscription subscription)
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

    protected override Task<CallResult<bool>> AuthenticateAsync(StreamConnection s)
    {
        throw new NotImplementedException();
    }
    #endregion

    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(Action<ParibuStreamTicker> onTickerData, Action<ParibuStreamPriceSeries> onPriceSeriesData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<ParibuStreamResponse>>(data =>
        {
            var json = JsonConvert.DeserializeObject<ParibuStreamPatch<ParibuStreamMerge<ParibuStreamTickers>>>(data.Data.Data);
            foreach (var ticker in json.Patch.Merge.Data)
            {
                if (ticker.Value.PriceSeries != null && ticker.Value.PriceSeries.Count() > 0)
                {
                    onPriceSeriesData(new ParibuStreamPriceSeries
                    {
                        Symbol = ticker.Key,
                        Prices = ticker.Value.PriceSeries,
                    });
                }
                else
                {
                    ticker.Value.Symbol = ticker.Key;
                    onTickerData(ticker.Value);
                }
            }
        });

        var request = new ParibuStreamRequest<ParibuSocketSubscribeRequest> { Event = "pusher:subscribe", Data = new ParibuSocketSubscribeRequest { Auth = "", Channel = "prb-public" } };
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarketDataAsync(string symbol, Action<ParibuStreamOrderBook> onOrderBookData, Action<ParibuStreamTrade> onTradeData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<ParibuStreamResponse>>(data =>
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

}