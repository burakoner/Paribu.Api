namespace Paribu.Api;

public partial class ParibuStreamClient : WebSocketApiClient
{
    #region Constructor
    public ParibuStreamClient() : this(null, new())
    {
    }

    public ParibuStreamClient(ILogger logger) : this(logger, new())
    {
    }

    public ParibuStreamClient(ParibuStreamClientOptions options) : this(null, options)
    {
    }

    public ParibuStreamClient(ILogger? logger, ParibuStreamClientOptions options) : base(logger ?? BaseClient.LoggerFactory.CreateLogger(typeof(ParibuStreamClient)), options)
    {
    }
    #endregion

    #region Overrided Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthentication(credentials);

    protected Task<CallResult<WebSocketUpdateSubscription>> PusherSubscribeAsync<T>(object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        return SubscribeAsync(ParibuAddress.Default.PusherStreamAddress, request, identifier, authenticated, dataHandler, ct);
    }

    protected Task<CallResult<WebSocketUpdateSubscription>> ParibuSubscribeAsync<T>(object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        return SubscribeAsync(ParibuAddress.Default.ParibuStreamAddress, request, identifier, authenticated, dataHandler, ct);
    }
    #endregion

    #region Protected Methods
    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T>? callResult)
    {
        callResult = null;
        return true;
    }

    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
    {
        callResult = null;

        // Paribu Stream
        try
        {
            var streamResponse = message.ToObject<ParibuStreamResponse>();
            if (streamResponse == null) return false;

            if (request is ParibuStreamRequest streamRequest)
            {
                if (streamRequest.Method == ParibuStreamRequestMethod.Subscribe &&
                    streamRequest.Id == streamResponse.Id &&
                    streamResponse.Code == 100)
                {
                    callResult = new CallResult<object>(true);
                    return true;
                }
            }
        }
        catch { }

        // Pusher Stream: Not implemented

        // Return
        return false;
    }

    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
    {
        if (message.Type != JTokenType.Object)
            return false;

        // Paribu Stream
        if (request is ParibuStreamRequest streamRequest && message["e"] != null && message["s"] != null)
        {
            // Ticker & Order Book
            var evt = (string)message["e"]!;
            var symbol = (string)message["s"]!;
            var channel = evt + ":" + symbol + "@100ms";
            if (streamRequest.Channels.Contains(channel))
                return true;
        }

        // Pusher Stream: Not implemented

        // Return
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

        // Paribu Stream
        if (subscription.Request is ParibuStreamRequest streamRequest)
        {
            var unsubscribeRequest = new ParibuStreamRequest
            {
                Method = ParibuStreamRequestMethod.Unsubscribe,
                Channels = streamRequest.Channels
            };
            _ = connection.SendAndWaitAsync(unsubscribeRequest, TimeSpan.FromMilliseconds(10), data =>
            {
                return true;
            });

            return true;
        }

        // Pusher Stream: Not implemented

        // Return
        return false;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection s)
    {
        throw new NotImplementedException();
    }
    #endregion

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickerAsync(string symbol, Action<ParibuStreamTicker> onData, CancellationToken ct = default)
        => SubscribeToTickerAsync([symbol], onData, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickerAsync(IEnumerable<string> symbols, Action<ParibuStreamTicker> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<ParibuStreamContainer<ParibuStreamTicker>>>(data =>
        {
            data.Data.Payload.Symbol = data.Data.Symbol;
            onData?.Invoke(data.Data.Payload);
        });

        var request = new ParibuStreamRequest
        {
            Method = ParibuStreamRequestMethod.Subscribe,
            Channels = [.. symbols.Select(x => "ticker24h:" + x + "@100ms")]
        };
        return ParibuSubscribeAsync(request, request.Id, false, internalHandler, ct);
    }


    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(string symbol, Action<ParibuStreamOrderBook> onData, CancellationToken ct = default)
        => SubscribeToOrderBookAsync([symbol], onData, ct);

    public Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(IEnumerable<string> symbols, Action<ParibuStreamOrderBook> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<ParibuStreamContainer<ParibuStreamOrderBook>>>(data =>
        {
            data.Data.Payload.Symbol = data.Data.Symbol;
            onData?.Invoke(data.Data.Payload);
        });

        var request = new ParibuStreamRequest
        {
            Method = ParibuStreamRequestMethod.Subscribe,
            Channels = [.. symbols.Select(x => "orderbook:" + x + "@100ms")]
        };
        return ParibuSubscribeAsync(request, request.Id, false, internalHandler, ct);
    }
}