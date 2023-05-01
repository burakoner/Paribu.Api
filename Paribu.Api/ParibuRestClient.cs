namespace Paribu.Api;

public class ParibuRestClient : RestApiClient
{
    #region Endpoints

    // Public Endpoints
    private const string v4_Public_Health_Endpoint = "health";                                      // GET
    private const string v4_Public_Config_Endpoint = "initials/config";                             // GET
    private const string v4_Public_Ticker_Endpoint = "initials/ticker";                             // GET
    private const string v4_Public_TickerExtended_Endpoint = "initials/ticker/extended";            // GET
    private const string v4_Public_PriceSeries_Endpoint = "initials/price-series";                  // GET
    private const string v4_Public_Orderbook_Endpoint = "market/{symbol}/orderbook";                // GET
    private const string v4_Public_LatestMatches_Endpoint = "market/{symbol}/latest-matches";       // GET

    // Public Contents
    private const string v4_Public_ContentsCountries_Endpoint = "contents/countries";               // GET
    private const string v4_Public_ContentsCities_Endpoint = "contents/cities";                     // GET
    private const string v4_Public_ContentsCounties_Endpoint = "contents/counties/{cityId}";        // GET
    private const string v4_Public_ContentsProfessions_Endpoint = "contents/professions";           // GET
    private const string v4_Public_ContentsBanners_Endpoint = "contents/banners";                   // GET
    private const string v4_Public_ContentsFeatures_Endpoint = "contents/features";                 // GET

    // Public Contents
    private const string v4_Chart_Config_Endpoint = "chart/config";                                 // GET
    private const string v4_Chart_History_Endpoint = "chart/history";                               // GET

    // Auth Endpoints
    private const string v4_Auth_Signup_Endpoint = "auth/sign-up";                                  // POST
    private const string v4_Auth_SignupEmail_Endpoint = "auth/sign-up-email";                       // POST
    private const string v4_Auth_VerifyEmail_Endpoint = "user/verify-email";                        // POST
    private const string v4_Auth_Verification_Endpoint = "user/verification";
    private const string v4_Auth_Forget_Endpoint = "auth/forget";
    private const string v4_Auth_NewPassword_Endpoint = "auth/new-password";
    private const string v4_Auth_ChangePassword_Endpoint = "auth/change-password";
    private const string v4_Auth_Signin_Endpoint = "auth/sign-in";                                  // POST
    private const string v4_Auth_Signout_Endpoint = "auth/sign-out";

    // MFA Endpoints
    private const string v4_MFA_Resend_Endpoint = "mfa/resend";                                     // POST
    private const string v4_MFA_Verify_Endpoint = "mfa/verify";                                     // POST

    // Private Endpoints
    private const string v4_Private_User_Endpoint = "user";                                         // GET
    private const string v4_Private_Transactions_Endpoint = "user/wallet/{asset}/transactions";     // GET
    private const string v4_Private_Unregister_Endpoint = "user/unregister";
    private const string v4_Private_G2faEnable_Endpoint = "user/g2fa-enable";
    private const string v4_Private_G2faDisable_Endpoint = "user/g2fa-disable";
    private const string v4_Private_ChangeEmail_Endpoint = "user/change-email";
    private const string v4_Private_Deactivate_Endpoint = "user/deactivate";
    private const string v4_Private_PusherAuthentication_Endpoint = "user/pusher/auth";             // POST  Request: socket_id=xxxxxx.xxxxxxx&channel_name=private-market-usdt_tl-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx     Response: {"auth":"xxxxxxxxxxxxxxxxxxxx:xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"}

    // Order Endpoints (Private)
    private const string v4_Orders_Endpoint = "orders";                                             // POST
    private const string v4_OrderGet_Endpoint = "orders/{id}";                                      // GET
    private const string v4_OrdersCancel_Endpoint = "orders/cancel";                                // POST
    private const string v4_OrdersCancelAll_Endpoint = "orders/cancel/all";                         // POST
    private const string v4_UserMarketOrders_Endpoint = "user/market/{symbol}/orders";              // GET
    private const string v4_OrdersHistory_Endpoint = "history";                                     // GET

    // Alarm Endpoints (Private)
    private const string v4_Alarm_Set_Endpoint = "alarm";                                           // POST
    // Request : {"market":"usdt_tl","trigger_price":"21.000"}
    // Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_set","params":{"market":"USDT-TL"}},"severity":"success","buttons":[{"severity":"dark","action":{"name":"close","target":"_self"},"label":{"langkey":"system_messages.close"}}]},"payload":{"uid":"e1dwjk9p-x85r-756w-dz21-lg26yovz34n0","user_uid":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx","market":"usdt_tl","direction":"up","trigger_price":21,"creation_price":20.77,"created_at":"2023-05-01T11:13:35.000000Z"},"meta":null}
    // Request : {"market":"usdt_tl","trigger_price":"20.000"}
    // Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_set","params":{"market":"USDT-TL"}},"severity":"success","buttons":[{"severity":"dark","action":{"name":"close","target":"_self"},"label":{"langkey":"system_messages.close"}}]},"payload":{"uid":"znp8v5k6-xw2m-qmdk-nv14-q3ojyge490d1","user_uid":"xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx","market":"usdt_tl","direction":"down","trigger_price":20,"creation_price":20.769,"created_at":"2023-05-01T11:14:30.000000Z"},"meta":null}

    private const string v4_Alarm_Delete_Endpoint = "alarm/{id}";                                   // DELETE
    // https://web.paribu.com/alarm/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
    // {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.alarm_canceled","params":{"market":"USDT-TL"}},"severity":"success"},"payload":{"deleted":true},"meta":null}

    private const string v4_Alarm_DeleteAll_Endpoint = "alarm/all";                                 // DELETE

    // Address Endpoints (Private)
    private const string v4_Address_Endpoint = "addresses";
    private const string v4_AddressAssign_Endpoint = "addresses/assign";
    private const string v4_AddressValidate_Endpoint = "validate/address";
    private const string v4_Address_Delete_Endpoint = "addresses/{id}";                             // DELETE
    private const string v4_Address_DeleteAll_Endpoint = "alarm/all";                               // DELETE

    // Notification Endpoints (Private)
    private const string v4_Notification_Endpoint = "notification";
    private const string v4_NotificationSettings_Endpoint = "notification/settings";
    private const string v4_NotificationPushToken_Endpoint = "notification/push-token";
    private const string v4_NotificationRead_Endpoint = "notification/read/{id}";
    private const string v4_NotificationReadAll_Endpoint = "notification/read/all";

    // Address Endpoints (Private)
    private const string v4_AnnouncementSettings_Endpoint = "announcement/settings";

    // Withdrawal Endpoints (Private)
    private const string v4_Withdraws_Endpoint = "withdraws";
    private const string v4_WithdrawsCancel_Endpoint = "withdraws/{id}";                            // DELETE

    // Favorite Endpoints (Private)
    private const string v4_Favorite_Endpoint = "favorite";                                         // POST (Add/Remove)    Request: {"market":"usdt_tl"}    Response: {"message":{"display":{"component":"snackbar","content":"status"},"title":{"langkey":"system_messages.favorite_added","params":{"market":"USDT-TL"}},"severity":"success"},"payload":null,"meta":null}

    // Donations Endpoints
    private const string v4_Donations_Initials_Endpoint = "donations/initial";
    private const string v4_Donations_Donate_Endpoint = "donations/donate";
    #endregion

    #region Constructor
    public ParibuRestClient() : this(ParibuRestClientOptions.Default)
    {
    }

    public string DeviceId { get; set; }
    public ParibuRestClient(ParibuRestClientOptions options) : base("Paribu (Unofficial) Rest Api", options)
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
    #endregion

    #region Overrided Methods
    protected override Error ParseErrorResponse(JToken error)
    {
        if (error["message"] == null)
            return new ServerError(error.ToString());

        if (error["message"].ToString() == "Forbidden")
            return new ServerError(-1, "Forbidden");

        if (error["message"]["title"] == null && error["message"]["description"] == null)
            return new ServerError(error.ToString());

        if (error["message"]["title"]["langkey"] == null && error["message"]["description"]["langkey"] == null)
            return new ServerError(error.ToString());

        var errorMessage =
        $"Title: {error["message"]?["title"]?["langkey"]}\n" +
        $"Description: {error["message"]?["description"]?["langkey"]}";

        return new ServerError(-1, errorMessage);
    }

    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthenticationProvider(credentials);
    #endregion

    #region Internal Methods
    internal Dictionary<string, string> ParibuAppHeaders()
    {
        /*
        Valid Devices
        - Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36 Edg/111.0.1661.62
        - test
        - Android
        - Windows

        Valid Platforms
        - Android
        - Windows
        */
        return new Dictionary<string, string>
        {
            { "user-agent", "ParibuApp/403 (Android 12)" },
            { "platform", "Android" },
            { "device", "Android" },
            { "version", "4.0.3" },
            { "pragma-cache-local", this.DeviceId }
        };
    }

    internal Uri GetUri(string endpoint) => new Uri($"{ClientOptions.BaseAddress.TrimEnd('/')}/{endpoint}");

    internal async Task<RestCallResult<string>> ExecuteAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
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

    internal async Task<RestCallResult<T>> ExecuteAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
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

    internal async Task<RestCallResult<T>> SendParibuRequestAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
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
        var result = await SendRequestAsync<ParibuRestApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        // Set Orifinal Cultures
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentUICulture;

        // Return
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
        return result.As(result.Data.Payload);
    }
    #endregion

    #region Api Methods
    public async Task<RestCallResult<bool>> GetHealthAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<IEnumerable<object>>(GetUri(v4_Public_Health_Endpoint), HttpMethod.Get, ct).ConfigureAwait(false);
        if (!result.Success) return result.AsError<bool>(result.Error);

        return result.As(true);
    }

    public async Task<RestCallResult<ParibuExchangeInformation>> GetExchangeInformationAsync(CancellationToken ct = default)
        => await SendParibuRequestAsync<ParibuExchangeInformation>(GetUri(v4_Public_Config_Endpoint), HttpMethod.Get, ct).ConfigureAwait(false);

    public async Task<RestCallResult<Dictionary<string, ParibuTicker>>> GetTickersAsync(CancellationToken ct = default)
        => await SendParibuRequestAsync<Dictionary<string, ParibuTicker>>(GetUri(v4_Public_Ticker_Endpoint), HttpMethod.Get, ct).ConfigureAwait(false);

    public async Task<RestCallResult<Dictionary<string, IEnumerable<decimal>>>> GetPriceSeriesAsync(CancellationToken ct = default)
        => await SendParibuRequestAsync<Dictionary<string, IEnumerable<decimal>>>(GetUri(v4_Public_PriceSeries_Endpoint), HttpMethod.Get, ct).ConfigureAwait(false);

    public async Task<RestCallResult<ParibuOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        => await SendParibuRequestAsync<ParibuOrderBook>(GetUri(v4_Public_Orderbook_Endpoint.Replace("{symbol}", symbol)), HttpMethod.Get, ct).ConfigureAwait(false);

    public async Task<RestCallResult<Dictionary<string, ParibuMatch>>> GetLatestMatchesAsync(string symbol, CancellationToken ct = default)
        => await SendParibuRequestAsync<Dictionary<string, ParibuMatch>>(GetUri(v4_Public_LatestMatches_Endpoint.Replace("{symbol}", symbol)), HttpMethod.Get, ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<ParibuKline>>> GetKlinesAsync(string symbol, ParibuKlineInterval interval, DateTime start, DateTime end, int limit, CancellationToken ct = default)
        => await GetKlinesAsync(symbol, interval, start.ConvertToMilliseconds(), end.ConvertToMilliseconds(), limit, ct).ConfigureAwait(false);

    /// <summary>
    /// Gets Symbol Klines
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="interval"></param>
    /// <param name="start">Epoch in Seconds</param>
    /// <param name="end">Epoch in Seconds</param>
    /// <param name="limit"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<ParibuKline>>> GetKlinesAsync(string symbol, ParibuKlineInterval interval, long start, long end, int limit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
            { "resolution", interval.GetLabel() },
            { "countback", limit },
            { "from", start },
            { "to", end },
        };

        var result = await ExecuteAsync<ParibuChartHistory>(GetUri(v4_Chart_History_Endpoint), HttpMethod.Get, ct, false, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.AsError<IEnumerable<ParibuKline>>(result.Error);
        return result.As(ParibuKline.ImportChartHistory(result.Data));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="countryCode">+90</param>
    /// <param name="password"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<ParibuMfaStatus>> LoginAsync(string countryCode, string mobile, string password, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "country_code", countryCode},
            { "mobile", mobile},
            { "password", password},
        };

        return await SendParibuRequestAsync<ParibuMfaStatus>(GetUri(v4_Auth_Signin_Endpoint), HttpMethod.Post, ct, false, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<ParibuAuthToken>> LoginVerifyAsync(string token, string code, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "token", token},
            { "code", code},
        };

        return await SendParibuRequestAsync<ParibuAuthToken>(GetUri(v4_MFA_Verify_Endpoint), HttpMethod.Post, ct, false, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<ParibuUserAccount>> GetUserAccountAsync(CancellationToken ct = default)
        => await SendParibuRequestAsync<ParibuUserAccount>(GetUri(v4_Private_User_Endpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);

    public async Task<RestCallResult<ParibuOrder>> PlaceOrderAsync(string symbol, ParibuOrderSide side, ParibuOrderType type, decimal? price = null, decimal? condition = null, decimal? amount = null, decimal? total = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "market", symbol},
            { "trade", side.GetLabel() },
            { "type", type.GetLabel() },
        };
        parameters.AddOptionalParameter("price", price);
        parameters.AddOptionalParameter("condition", condition);
        parameters.AddOptionalParameter("amount", amount);
        parameters.AddOptionalParameter("total", total);

        return await SendParibuRequestAsync<ParibuOrder>(GetUri(v4_Orders_Endpoint), method: HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<ParibuOrder>> GetOrderAsync(string orderId, CancellationToken ct = default)
        => await SendParibuRequestAsync<ParibuOrder>(GetUri(v4_OrderGet_Endpoint.Replace("{id}", orderId)), method: HttpMethod.Get, ct, signed: true).ConfigureAwait(false);

    public async Task<RestCallResult<ParibuCancelResponse>> CancelOrderAsync(string orderId, CancellationToken ct = default)
        => await CancelOrdersAsync(new List<string> { orderId }, ct).ConfigureAwait(false);

    public async Task<RestCallResult<ParibuCancelResponse>> CancelOrdersAsync(IEnumerable<string> orderIds, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ids", orderIds},
        };

        return await SendParibuRequestAsync<ParibuCancelResponse>(GetUri(v4_OrdersCancel_Endpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<ParibuCancelResponse>> CancelAllOrdersAsync(CancellationToken ct = default)
        => await SendParibuRequestAsync<ParibuCancelResponse>(GetUri(v4_OrdersCancelAll_Endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<ParibuOrder>>> GetOrdersAsync(string symbol, CancellationToken ct = default)
        => await SendParibuRequestAsync<IEnumerable<ParibuOrder>>(GetUri(v4_UserMarketOrders_Endpoint.Replace("{symbol}", symbol)), method: HttpMethod.Get, ct, signed: true).ConfigureAwait(false);

    /// <summary>
    /// Query Order History
    /// </summary>
    /// <param name="processes">Valid Values: buy, sell, deposit, withdraw</param>
    /// <param name="assets">Asset Filter</param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<RestCallResult<IEnumerable<ParibuOrder>>> GetOrdersHistoryAsync(List<string> processes, List<string> assets = null, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int rows = 25, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "processes", processes },
            { "page", page },
            { "per_page", rows },
        };
        if (assets != null && assets.Any()) parameters.AddOptionalParameter("currencies", assets);
        parameters.AddOptionalParameter("started_at", startDate);
        parameters.AddOptionalParameter("ended_at", endDate);

        return await SendParibuRequestAsync<IEnumerable<ParibuOrder>>(GetUri(v4_OrdersHistory_Endpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    #endregion
}