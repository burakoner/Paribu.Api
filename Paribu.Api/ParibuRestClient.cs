namespace Paribu.Api;

public class ParibuRestClient : RestApiClient
{
    #region Legacy Endpoints
    // Public Endpoints
    private const string _v4PublicHealthEndpoint = "health";                                      // GET
    private const string _v4PublicConfigEndpoint = "initials/config";                             // GET
    private const string _v4PublicTickerEndpoint = "initials/ticker";                             // GET
    private const string _v4PublicTickerExtendedEndpoint = "initials/ticker/extended";            // GET
    private const string _v4PublicPriceSeriesEndpoint = "initials/price-series";                  // GET
    private const string _v4PublicOrderbookEndpoint = "market/{symbol}/orderbook";                // GET
    private const string _v4PublicLatestMatchesEndpoint = "market/{symbol}/latest-matches";       // GET

    // Public Contents Endpoints
    private const string _v4PublicContentsCountriesEndpoint = "contents/countries";               // GET
    private const string _v4PublicContentsCitiesEndpoint = "contents/cities";                     // GET
    private const string _v4PublicContentsCountiesEndpoint = "contents/counties/{cityId}";        // GET
    private const string _v4PublicContentsProfessionsEndpoint = "contents/professions";           // GET
    private const string _v4PublicContentsBannersEndpoint = "contents/banners";                   // GET
    private const string _v4PublicContentsFeaturesEndpoint = "contents/features";                 // GET

    // Chart Contents Endpoints
    private const string _v4ChartConfigEndpoint = "chart/config";                                 // GET
    private const string _v4ChartHistoryEndpoint = "chart/history";                               // GET

    // Auth Endpoints
    private const string _v4AuthSignupEndpoint = "auth/sign-up";                                  // POST
    private const string _v4AuthSignupEmailEndpoint = "auth/sign-up-email";                       // POST
    private const string _v4AuthVerifyEmailEndpoint = "user/verify-email";                        // POST
    private const string _v4AuthVerificationEndpoint = "user/verification";
    private const string _v4AuthForgetEndpoint = "auth/forget";
    private const string _v4AuthNewPasswordEndpoint = "auth/new-password";
    private const string _v4AuthChangePasswordEndpoint = "auth/change-password";
    private const string _v4AuthSigninEndpoint = "auth/sign-in";                                  // POST
    private const string _v4AuthSignoutEndpoint = "auth/sign-out";

    // MFA Endpoints
    private const string _v4MFAResendEndpoint = "mfa/resend";                                     // POST
    private const string _v4MFAVerifyEndpoint = "mfa/verify";                                     // POST

    // Private Endpoints
    private const string _v4PrivateUserEndpoint = "user";                                         // GET
    private const string _v4PrivateTransactionsEndpoint = "user/wallet/{asset}/transactions";     // GET
    private const string _v4PrivateUnregisterEndpoint = "user/unregister";
    private const string _v4PrivateG2faEnableEndpoint = "user/g2fa-enable";
    private const string _v4PrivateG2faDisableEndpoint = "user/g2fa-disable";
    private const string _v4PrivateChangeEmailEndpoint = "user/change-email";
    private const string _v4PrivateDeactivateEndpoint = "user/deactivate";
    private const string _v4PrivatePusherAuthenticationEndpoint = "user/pusher/auth";             // POST  Request: socket_id=xxxxxx.xxxxxxx&channel_name=private-market-usdt_tl-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx     Response: {"auth":"xxxxxxxxxxxxxxxxxxxx:xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"}

    // Order Endpoints (Private)
    private const string _v4OrdersEndpoint = "orders";                                             // POST
    private const string _v4OrderGetEndpoint = "orders/{id}";                                      // GET
    private const string _v4OrdersCancelEndpoint = "orders/cancel";                                // POST
    private const string _v4OrdersCancelAllEndpoint = "orders/cancel/all";                         // POST
    private const string _v4UserMarketOrdersEndpoint = "user/market/{symbol}/orders";              // GET
    private const string _v4OrdersHistoryEndpoint = "history";                                     // GET

    // Alarm Endpoints (Private)
    private const string _v4AlarmSetEndpoint = "alarm";                                           // POST
    // Request : {"market":"usdt_tl","trigger_price":"21.000"}
    // Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_set","params":{"market":"USDT-TL"}},"severity":"success","buttons":[{"severity":"dark","action":{"name":"close","target":"_self"},"label":{"langkey":"system_messages.close"}}]},"payload":{"uid":"e1dwjk9p-x85r-756w-dz21-lg26yovz34n0","user_uid":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx","market":"usdt_tl","direction":"up","trigger_price":21,"creation_price":20.77,"created_at":"2023-05-01T11:13:35.000000Z"},"meta":null}
    // Request : {"market":"usdt_tl","trigger_price":"20.000"}
    // Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_set","params":{"market":"USDT-TL"}},"severity":"success","buttons":[{"severity":"dark","action":{"name":"close","target":"_self"},"label":{"langkey":"system_messages.close"}}]},"payload":{"uid":"znp8v5k6-xw2m-qmdk-nv14-q3ojyge490d1","user_uid":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx","market":"usdt_tl","direction":"down","trigger_price":20,"creation_price":20.769,"created_at":"2023-05-01T11:14:30.000000Z"},"meta":null}

    private const string _v4AlarmDeleteEndpoint = "alarm/{id}";                                   // DELETE
    // https://web.paribu.com/alarm/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    // {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_canceled","params":{"market":"USDT-TL"}},"severity":"success"},"payload":{"deleted":true},"meta":null}

    private const string _v4AlarmDeleteAllEndpoint = "alarm/all";                                 // DELETE

    // Address Endpoints (Private)
    private const string _v4AddressEndpoint = "addresses";
    private const string _v4AddressAssignEndpoint = "addresses/assign";
    private const string _v4AddressValidateEndpoint = "validate/address";
    private const string _v4AddressDeleteEndpoint = "addresses/{id}";                             // DELETE
    private const string _v4AddressDeleteAllEndpoint = "alarm/all";                               // DELETE

    // Notification Endpoints (Private)
    private const string _v4NotificationEndpoint = "notification";
    private const string _v4NotificationSettingsEndpoint = "notification/settings";
    private const string _v4NotificationPushTokenEndpoint = "notification/push-token";
    private const string _v4NotificationReadEndpoint = "notification/read/{id}";
    private const string _v4NotificationReadAllEndpoint = "notification/read/all";

    // Address Endpoints (Private)
    private const string _v4AnnouncementSettingsEndpoint = "announcement/settings";

    // Withdrawal Endpoints (Private)
    private const string _v4WithdrawsEndpoint = "withdraws";
    private const string _v4WithdrawsCancelEndpoint = "withdraws/{id}";                            // DELETE

    // Favorite Endpoints (Private)
    private const string _v4FavoriteEndpoint = "favorite";                                         // POST (Add/Remove)    Request: {"market":"usdt_tl"}    Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.favorite_added","params":{"market":"USDT-TL"}},"severity":"success"},"payload":null,"meta":null}

    // Donations Endpoints
    private const string _v4DonationsInitialsEndpoint = "donations/initial";
    private const string _v4DonationsDonateEndpoint = "donations/donate";
    #endregion

    #region API Endpoints
    // Market Data Endpoints
    private const string _apiOrderbookEndpoint = "orderbook";                   // GET
    private const string _apiMarketTickerEndpoint = "market/ticker";            // GET

    // Account Endpoints
    private const string _apiUsersMeEndpoint = "users/me";                      // GET
    private const string _apiUserAssetsEndpoint = "user/assets";                // GET
    private const string _apiTransfersHistoryEndpoint = "transfers/history";    // GET
    private const string _apiTradesHistoryEndpoint = "trades/history";          // GET

    // Deposit and Withdrawals Endpoints
    private const string _apiAddressesAssignEndpoint = "addresses/assign";      // POST

    // Orders Endpoints
    private const string _apiOrderEndpoint = "order";                           // POST - DELETE - GET
    private const string _apiOpenOrdersEndpoint = "open-orders";                // GET
    #endregion

    public string DeviceId { get; set; }

    #region Constructor
    public ParibuRestClient() : this(null, new())
    {
    }

    public ParibuRestClient(ILogger logger) : this(logger, new())
    {
    }

    public ParibuRestClient(ParibuRestClientOptions options) : this(null, options)
    {
    }

    public ParibuRestClient(ILogger? logger, ParibuRestClientOptions options) : base(logger ?? BaseClient.LoggerFactory.CreateLogger(typeof(ParibuRestClient)), options)
    {
        DeviceId = Guid.NewGuid().ToString().Replace("-", "");
    }
    #endregion

    #region Public Methods
    public void SetDeviceId(string deviceId)
    {
        this.DeviceId = deviceId;
    }

    public void SetAccessToken(string token)
    {
        SetApiCredentials(new ApiCredentials(token, "-----DUMMY-SECRET-----"));
    }

    public void SetApiCredentials(string apikey, string secret)
    {
        SetApiCredentials(new ApiCredentials(apikey, secret));
    }
    #endregion

    #region Overrided Methods
    protected override Error ParseErrorResponse(JToken error)
    {
        // API
        if (error["code"] != null && error["message"] != null)
            return new ServerError((int)error["code"]!, (string)error["message"]!);

        // App
        if (error["message"]?["title"]?["langkey"] != null && error["message"]?["description"]?["langkey"] != null)
        {
            var errorMessage =
            $"Title: {error["message"]?["title"]?["langkey"]}\n" +
            $"Description: {error["message"]?["description"]?["langkey"]}";
            return new ServerError(-1, errorMessage);
        }

        // Generic
        return new ServerError(error.ToString());
    }

    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthentication(credentials);
    #endregion

    #region Internal Methods
    internal Dictionary<string, string> ParibuAppHeaders()
    {
        /*
        Valid Devices
        - Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36 Edg/111.0.1661.62
        - Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36 Edg/125.0.0.0
        - test
        - Android
        - Windows

        Valid Platforms
        - Android
        - Windows
        */
        return new Dictionary<string, string>
        {
            { "user-agent", "ParibuApp/440 (Android 13)" },
            { "platform", "Android" },
            { "device", "Android" },
            { "version", "4.4.0" },
            { "pragma-cache-local", this.DeviceId }
        };
    }

    internal Uri GetUri(string api, string endpoint) => new($"{api.TrimEnd('/')}/{endpoint}");

    internal async Task<RestCallResult<string>> ExecuteAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? serialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
    {
        // Get Original Cultures
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var currentUICulture = Thread.CurrentThread.CurrentUICulture;

        // Set Cultures
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        // Do Request
        if (headerParameters == null) headerParameters = ParibuAppHeaders();
        else ParibuAppHeaders().ToList().ForEach(x => headerParameters[x.Key] = x.Value);
        var result = await SendRequestAsync<string>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Set Orifinal Cultures
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentUICulture;

        // Return
        if (!result) return result.AsError<string>(result.Error!);
        return result.As(result.Data);
    }

    internal async Task<RestCallResult<T>> ExecuteAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? serialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
    {
        // Get Original Cultures
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var currentUICulture = Thread.CurrentThread.CurrentUICulture;

        // Set Cultures
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        // Do Request
        if (headerParameters == null) headerParameters = ParibuAppHeaders();
        else ParibuAppHeaders().ToList().ForEach(x => headerParameters[x.Key] = x.Value);
        var result = await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Set Orifinal Cultures
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentUICulture;

        // Return
        if (!result) return result.AsError<T>(result.Error!);
        return result.As(result.Data);
    }

    internal async Task<RestCallResult<T>> ExecuteAppRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? serialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
    {
        // Get Original Cultures
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var currentUICulture = Thread.CurrentThread.CurrentUICulture;

        // Set Cultures
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        // Do Request
        if (headerParameters == null) headerParameters = ParibuAppHeaders();
        else ParibuAppHeaders().ToList().ForEach(x => headerParameters[x.Key] = x.Value);
        var result = await SendRequestAsync<ParibuRestAppResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Set Orifinal Cultures
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentUICulture;

        // Return Error
        if (!result) return result.AsError<T>(result.Error!);
        if (result.Data.Payload == null)
        {
            if (result.Data.Message != null)
            {
                if ((result.Data.Message.Title != null || result.Data.Message.Description != null))
                {
                    var errorMessage =
                        $"Title: {result.Data.Message.Title?.LanguageKey}\n" +
                        $"Description: {result.Data.Message.Description?.LanguageKey}";
                    return result.AsError<T>(new CallError(errorMessage));
                }
            }
        }

        // Return Data
        return result.As(result.Data.Payload!);
    }

    internal async Task<RestCallResult<T>> ExecuteApiRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object>? queryParameters = null, Dictionary<string, object>? bodyParameters = null, Dictionary<string, string>? headerParameters = null, ArraySerialization? serialization = null, JsonSerializer? deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
    {
        // Get Original Cultures
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        var currentUICulture = Thread.CurrentThread.CurrentUICulture;

        // Set Cultures
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        // Do Request
        var result = await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Set Orifinal Cultures
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentUICulture;

        // Return Data
        return result;
    }
    #endregion

    #region Public Methods
    public async Task<RestCallResult<bool>> PingAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<List<object>>(GetUri(ParibuAddress.Default.WebAddress, _v4PublicHealthEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success || result.Data == null) return result.AsError<bool>(result.Error!);

        return result.As(true);
    }

    public Task<RestCallResult<ParibuAppInformation>> GetExchangeInformationAsync(CancellationToken ct = default)
        => ExecuteAppRequestAsync<ParibuAppInformation>(GetUri(ParibuAddress.Default.WebAddress, _v4PublicConfigEndpoint), HttpMethod.Get, ct);

#if DEBUG
    public async Task<RestCallResult<List<ParibuAppTicker>>> GetAppTickersAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAppRequestAsync<Dictionary<string, ParibuAppTicker>>(GetUri(ParibuAddress.Default.WebAddress, _v4PublicTickerEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success || result.Data == null) return result.AsError<List<ParibuAppTicker>>(result.Error!);

        return result.As(result.Data.Select(x => x.Value).ToList());
    }

    public Task<RestCallResult<ParibuAppOrderBook>> GetAppOrderBookAsync(string symbol, CancellationToken ct = default)
        => ExecuteAppRequestAsync<ParibuAppOrderBook>(GetUri(ParibuAddress.Default.WebAddress, _v4PublicOrderbookEndpoint.Replace("{symbol}", symbol)), HttpMethod.Get, ct);
#endif

    public Task<RestCallResult<List<ParibuTicker>>> GetTickersAsync(CancellationToken ct = default)
        => ExecuteApiRequestAsync<List<ParibuTicker>>(GetUri(ParibuAddress.Default.ApiAddress, _apiMarketTickerEndpoint), HttpMethod.Get, ct);

    public async Task<RestCallResult<List<ParibuAppTrade>>> GetPublicTradesAsync(string symbol, CancellationToken ct = default)
    {
        var result = await ExecuteAppRequestAsync<Dictionary<string, ParibuAppTrade>>(GetUri(ParibuAddress.Default.WebAddress, _v4PublicLatestMatchesEndpoint.Replace("{symbol}", symbol)), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success || result.Data == null) return result.AsError<List<ParibuAppTrade>>(result.Error!);

        return result.As(result.Data.Select(x => x.Value).ToList());
    }

    public Task<RestCallResult<ParibuOrderBook>> GetOrderBookAsync(string symbol, int? depth = null, CancellationToken ct = default)
    {
        depth?.ValidateIntBetween(nameof(depth), 1, 20);

        var parameters = new ParameterCollection();
        parameters.AddParameter("market", symbol);
        parameters.AddOptional("depth", depth);

        return ExecuteApiRequestAsync<ParibuOrderBook>(GetUri(ParibuAddress.Default.ApiAddress, _apiOrderbookEndpoint), HttpMethod.Get, ct, queryParameters: parameters);
    }

    public async Task<RestCallResult<List<ParibuKline>>> GetKlinesAsync(string symbol, ParibuKlineInterval interval, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddEnum("period", interval);
        parameters.AddParameter("type", "basic");

        var result = await ExecuteAsync<ParibuChartHistory>(GetUri(ParibuAddress.Default.WebAddress, _v4ChartHistoryEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.AsError<List<ParibuKline>>(result.Error!);
        return result.As(ParibuKline.ImportChartHistory(result.Data));
    }

#if DEBUG
    public async Task<RestCallResult<List<ParibuKline>>> GetKlinesAsync(string symbol, ParibuKlineInterval interval, /*DateTime start,*/ DateTime end, CancellationToken ct = default)
        => await GetKlinesAsync(symbol, interval, /*start.ConvertToMilliseconds(),*/ end.ConvertToMilliseconds(), ct).ConfigureAwait(false);

    public async Task<RestCallResult<List<ParibuKline>>> GetKlinesAsync(string symbol, ParibuKlineInterval interval, /*long start,*/ long end, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("symbol", symbol);
        parameters.AddEnum("resolution", interval);
        // parameters.AddParameter("from", start);
        parameters.AddParameter("to", end);

        var result = await ExecuteAsync<ParibuChartHistory>(GetUri(ParibuAddress.Default.WebAddress, _v4ChartHistoryEndpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.AsError<List<ParibuKline>>(result.Error!);
        return result.As(ParibuKline.ImportChartHistory(result.Data));
    }
#endif
    public Task<RestCallResult<ParibuUserInformation>> GetUserInformationAsync(CancellationToken ct = default)
        => ExecuteApiRequestAsync<ParibuUserInformation>(GetUri(ParibuAddress.Default.ApiAddress, _apiUsersMeEndpoint), HttpMethod.Get, ct, true);

    public Task<RestCallResult<List<ParibuBalance>>> GetBalancesAsync(CancellationToken ct = default)
        => ExecuteApiRequestAsync<List<ParibuBalance>>(GetUri(ParibuAddress.Default.ApiAddress, _apiUserAssetsEndpoint), HttpMethod.Get, ct, true);

    public Task<RestCallResult<ParibuTransferHistory>> GetTransfersAsync(DateTime? beginDate = null, DateTime? endDate = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("begin_date", beginDate?.ToString("yyyy-MM-dd"));
        parameters.AddOptional("end_date", endDate?.ToString("yyyy-MM-dd"));

        return ExecuteApiRequestAsync<ParibuTransferHistory>(GetUri(ParibuAddress.Default.ApiAddress, _apiTransfersHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<ParibuTradeHistory>> GetUserTradesAsync(string? symbol = null, ParibuOrderSide? side = null, DateTime? beginDate = null, DateTime? endDate = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("filter_market", symbol);
        parameters.AddOptionalEnum("filter_direction", side);
        parameters.AddOptional("begin_date", beginDate?.ToString("yyyy-MM-dd"));
        parameters.AddOptional("end_date", endDate?.ToString("yyyy-MM-dd"));

        return ExecuteApiRequestAsync<ParibuTradeHistory>(GetUri(ParibuAddress.Default.ApiAddress, _apiTradesHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<ParibuOrder>> PlaceOrderAsync(string symbol, ParibuOrderType type, ParibuOrderSide side, decimal? amount = null, decimal? total = null, decimal? price = null, decimal? condition = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("market", symbol);
        parameters.AddEnum("type", type);
        parameters.AddEnum("trade", side);
        parameters.AddOptional("amount", amount);
        parameters.AddOptional("total", total);
        parameters.AddOptional("price", price);
        parameters.AddOptional("condition", condition);

        return ExecuteApiRequestAsync<ParibuOrder>(GetUri(ParibuAddress.Default.ApiAddress, _apiOrderEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    public Task<RestCallResult<ParibuOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
    {
        return ExecuteApiRequestAsync<ParibuOrder>(GetUri(ParibuAddress.Default.ApiAddress, _apiOrderEndpoint.AppendPath(orderId)), HttpMethod.Get, ct, true);
    }

    public async Task<RestCallResult<bool>> CancelOrderAsync(string orderId, CancellationToken ct = default)
    {
        var result = await ExecuteApiRequestAsync<string>(GetUri(ParibuAddress.Default.ApiAddress, _apiOrderEndpoint.AppendPath(orderId)), HttpMethod.Delete, ct, true);
        if (!result.Success || result.Data == null) return result.AsError<bool>(result.Error!);

        return result.As(true);
    }

    public Task<RestCallResult<List<ParibuOrder>>> GetOpenOrderAsync(string? symbol = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("market", symbol);

        return ExecuteApiRequestAsync<List<ParibuOrder>>(GetUri(ParibuAddress.Default.ApiAddress, _apiOpenOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    public Task<RestCallResult<ParibuDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddParameter("currency", asset);
        parameters.AddParameter("network", network);

        return ExecuteApiRequestAsync<ParibuDepositAddress>(GetUri(ParibuAddress.Default.ApiAddress, _apiAddressesAssignEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    #endregion
}