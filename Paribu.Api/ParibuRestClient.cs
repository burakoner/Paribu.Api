namespace Paribu.Api;

public class ParibuRestClient : RestApiClient
{
    #region Internal Fields
    public string DeviceId { get; set; }
    #endregion

    #region Endpoints
    protected const int ParibuErrorCode = -1;

    protected const string Endpoints_Public_Initials = "initials";                              // GET
    protected const string Endpoints_Public_Ticker = "ticker";                                  // GET
    protected const string Endpoints_Public_Market = "markets/{symbol}";                        // GET
    protected const string Endpoints_Public_Chart = "charts/{symbol}";                          // GET
    protected const string Endpoints_Public_Register = "register";                              // POST
    protected const string Endpoints_Public_TwoFactor = "two-factor";                           // POST
    protected const string Endpoints_Public_Login = "login";                                    // POST
    // protected const string Endpoints_Public_RetrySms = "retry-sms";                          // POST
    // protected const string Endpoints_Public_ResetPasswordInit = "reset/password";            // POST
    // protected const string Endpoints_Public_ResetPasswordAction = "reset/password";          // PUT
    // protected const string Endpoints_Public_EmailConfirmation = "user/email/confirmation";   // POST

    // protected const string Endpoints_Private_IdVerify = "user/id-verify";                    // POST
    // protected const string Endpoints_Private_Logout = "user/logout";                         // POST
    protected const string Endpoints_Private_OpenOrders = "user/orders";                        // GET
    protected const string Endpoints_Private_PlaceOrder = "user/orders";                        // POST
    protected const string Endpoints_Private_CancelOrder = "user/orders/{uid}";                 // DELETE
    protected const string Endpoints_Private_SetUserAlert = "user/alerts";                      // POST
    protected const string Endpoints_Private_DeleteUserAlert = "user/alerts/{uid}";             // DELETE
    // protected const string Endpoints_Private_GetDepositAdress = "user/addresses/assign";     // POST
    // protected const string Endpoints_Private_DeleteWithdrawAdress = "user/addresses/{t}";    // DELETE
    protected const string Endpoints_Private_Withdraw = "user/withdraws";                       // POST
    protected const string Endpoints_Private_CancelWithdrawal = "user/withdraws/{uid}";         // DELETE
    // protected const string Endpoints_Private_IninalWithdraw = "user/ininal/withdraw";        // POST
    // protected const string Endpoints_Private_ChangePassword = "user/password";               // PUT
    // protected const string Endpoints_Private_ChangeEmail = "user/email";                     // PUT
    // protected const string Endpoints_Private_ChangeTwoFactor = "user/two-factor";            // PUT
    // protected const string Endpoints_Private_CreateTicket = "tickets";                       // POST
    // protected const string Endpoints_Private_UserCards = "user/cards";                       // POST
    // protected const string Endpoints_Private_TicketToken = "ticket/token";                   // GET
    // protected const string Endpoints_Private_FenerbahceToken = "user/fb-token";              // POST
    #endregion

    #region Constructor
    public ParibuRestClient() : this(ParibuRestClientOptions.Default)
    {
    }

    public ParibuRestClient(ParibuRestClientOptions options) : base("Paribu Rest Api", options)
    {
        DeviceId = Guid.NewGuid().ToString();
    }
    #endregion

    #region Common Methods
    public virtual void SetDeviceId(string deviceId)
    {
        this.DeviceId = deviceId;
    }

    public virtual void SetAccessToken(string token)
    {
        SetApiCredentials(new ApiCredentials(token, "-----DUMMY-SECRET-----"));
    }
    #endregion

    #region Overrided Methods
    protected override Error ParseErrorResponse(JToken error)
    {
        if (error["message"] == null)
            return new ServerError(error.ToString());

        return new ServerError(ParibuErrorCode, (string)error["message"], null);
    }

    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new ParibuAuthenticationProvider(credentials);
    #endregion

    #region Internal Methods
    internal Uri GetUri(string endpoint)
    {
        return new Uri($"{ClientOptions.BaseAddress.TrimEnd('/')}/{endpoint}");
    }

    internal virtual Dictionary<string, string> ParibuAppHeaders()
    {
        return new Dictionary<string, string>
        {
            { "user-agent", "ParibuApp/345 (Android 12)" },
            { "x-app-version", "345" },
            { "x-device", "Paribu.Api" },
            { "pragma-cache-local", this.DeviceId }
        };
    }

    internal async Task<RestCallResult<T>> ExecuteAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        var result = await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
        if (!result) return result.AsError<T>(result.Error!);

        return result.As(result.Data);
    }
    #endregion

    #region Api Methods
    /// <summary>
    /// Gets Initials
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuInitials>> GetInitialsAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuInitials>>(GetUri(Endpoints_Public_Initials), method: HttpMethod.Get, ct, signed: false).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuInitials>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuInitials>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Gets all tickers
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<Dictionary<string, ParibuTicker>>> GetTickersAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTickers>>(GetUri(Endpoints_Public_Ticker), method: HttpMethod.Get, ct, signed: false).ConfigureAwait(false);
        if (!result.Success) return result.AsError<Dictionary<string, ParibuTicker>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<Dictionary<string, ParibuTicker>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data.Data);
    }

    /// <summary>
    /// Gets Market Data for a specific Symbol
    /// </summary>
    /// <param name="symbol">Market Symbol</param>
    /// <param name="interval">Chart Term</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuMarketData>> GetMarketDataAsync(string symbol, ChartInterval interval = ChartInterval.OneDay, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "interval", JsonConvert.SerializeObject(interval, new ChartIntervalConverter(false)) }
            };
        var result = await ExecuteAsync<ParibuRestApiResponse<MarketData>>(GetUri(Endpoints_Public_Market.Replace("{symbol}", symbol)), method: HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuMarketData>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuMarketData>(new ParibuRestApiError(null, string.Empty, null));

        var pmd = new ParibuMarketData
        {
            ChartData = new ParibuChartData(),
            OrderBook = new ParibuOrderBook(),
            MarketMatches = result.Data.Data.MarketMatches,
            UserMatches = result.Data.Data.UserMatches != null ? result.Data.Data.UserMatches : new List<ParibuUserMatch>(),
        };

        // Candles
        var min = result.Data.Data.ChartData.OpenTimeData.Count();
        min = Math.Min(min, result.Data.Data.ChartData.VolumeData.Count());
        min = Math.Min(min, result.Data.Data.ChartData.ClosePriceData.Count());
        pmd.ChartData.Symbol = result.Data.Data.ChartData.Symbol;
        pmd.ChartData.Interval = result.Data.Data.ChartData.Interval;
        for (var i = 0; i < min; i++)
        {
            pmd.ChartData.Candles.Add(new ParibuCandle
            {
                OpenTime = result.Data.Data.ChartData.OpenTimeData.ElementAt(i),
                OpenDateTime = result.Data.Data.ChartData.OpenTimeData.ElementAt(i).FromUnixTimeSeconds(),
                ClosePrice = result.Data.Data.ChartData.ClosePriceData.ElementAt(i),
                Volume = result.Data.Data.ChartData.VolumeData.ElementAt(i),
            });
        }

        // Order Book
        foreach (var ask in result.Data.Data.OrderBook.Asks.Data) pmd.OrderBook.Asks.Add(new ParibuOrderBookEntry { Price = ask.Key, Amount = ask.Value });
        foreach (var bid in result.Data.Data.OrderBook.Bids.Data) pmd.OrderBook.Bids.Add(new ParibuOrderBookEntry { Price = bid.Key, Amount = bid.Value });

        return result.As(pmd);
    }

    /// <summary>
    /// Gets Chart Data for a specific Symbol
    /// </summary>
    /// <param name="symbol">Market Symbol</param>
    /// <param name="interval">Chart Term</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuChartData>> GetChartDataAsync(string symbol, ChartInterval interval = ChartInterval.OneDay, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "interval", JsonConvert.SerializeObject(interval, new ChartIntervalConverter(false)) }
            };
        var result = await ExecuteAsync<ParibuRestApiResponse<ChartData>>(GetUri(Endpoints_Public_Chart.Replace("{symbol}", symbol)), method: HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuChartData>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuChartData>(new ParibuRestApiError(null, string.Empty, null));

        // Candles
        var chart = new ParibuChartData();
        var min = result.Data.Data.OpenTimeData.Count();
        min = Math.Min(min, result.Data.Data.VolumeData.Count());
        min = Math.Min(min, result.Data.Data.ClosePriceData.Count());
        chart.Symbol = result.Data.Data.Symbol;
        chart.Interval = result.Data.Data.Interval;
        for (var i = 0; i < min; i++)
        {
            chart.Candles.Add(new ParibuCandle
            {
                OpenTime = result.Data.Data.OpenTimeData.ElementAt(i),
                OpenDateTime = result.Data.Data.OpenTimeData.ElementAt(i).FromUnixTimeSeconds(),
                ClosePrice = result.Data.Data.ClosePriceData.ElementAt(i),
                Volume = result.Data.Data.VolumeData.ElementAt(i),
            });
        }

        // Return
        return result.As(chart);
    }

    /// <summary>
    /// Registers new user
    /// </summary>
    /// <param name="name">Name Surname</param>
    /// <param name="email">Email Address</param>
    /// <param name="mobile">10 digits Mobile Number (532xxxxxxx)</param>
    /// <param name="password">Account Password</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTwoFactorToken>> RegisterAsync(string name, string email, string mobile, string password, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "name", name},
                { "email", email},
                { "mobile", mobile},
                { "password", password},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTwoFactorToken>>(GetUri(Endpoints_Public_Register), method: HttpMethod.Post, ct, signed: false, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTwoFactorToken>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTwoFactorToken>(new ParibuRestApiError(null, string.Empty, null));

        result.Data.Data.Message = result.Data.Message;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Login Method
    /// </summary>
    /// <param name="mobile">10 digits Mobile Number (532xxxxxxx)</param>
    /// <param name="password">Account Password</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTwoFactorToken>> LoginAsync(string mobile, string password, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "mobile", mobile},
                { "password", password},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTwoFactorToken>>(GetUri(Endpoints_Public_Login), method: HttpMethod.Post, ct, signed: false, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTwoFactorToken>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTwoFactorToken>(new ParibuRestApiError(null, string.Empty, null));

        result.Data.Data.Message = result.Data.Message;
        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Register TFA Method
    /// </summary>
    /// <param name="token">Register TFA Token</param>
    /// <param name="code">PIN Code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTwoFactorUser>> RegisterTwoFactorAsync(string token, string code, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "token", token},
                { "code", code},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTwoFactorUser>>(GetUri(Endpoints_Public_TwoFactor), method: HttpMethod.Post, ct, signed: false, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTwoFactorUser>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTwoFactorUser>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// TFA Method for Login
    /// </summary>
    /// <param name="token">Login TFA Token</param>
    /// <param name="code">PIN Code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTwoFactorUser>> LoginTwoFactorAsync(string token, string code, bool setAccessToken = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "token", token},
                { "code", code},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTwoFactorUser>>(GetUri(Endpoints_Public_TwoFactor), method: HttpMethod.Post, ct, signed: false, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTwoFactorUser>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTwoFactorUser>(new ParibuRestApiError(null, string.Empty, null));

        if (setAccessToken && (result.Data.Data.Subject == TwoFactorSubject.Register || result.Data.Data.Subject == TwoFactorSubject.Login))
        {
            if (!string.IsNullOrEmpty(result.Data.Data.Token))
                SetAccessToken(result.Data.Data.Token);
        }

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// TFA Method for TFA Toggle Action
    /// </summary>
    /// <param name="token">Toggle TFA Token</param>
    /// <param name="code">PIN Code</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTwoFactorToggle>> ToggleTwoFactorAsync(string token, string code, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "token", token},
                { "code", code},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTwoFactorToggle>>(GetUri(Endpoints_Public_TwoFactor), method: HttpMethod.Post, ct, signed: false, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTwoFactorToggle>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTwoFactorToggle>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Gets Open Orders
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<ParibuOrder>>> GetOpenOrdersAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<Dictionary<string, ParibuOrder>>>(GetUri(Endpoints_Private_OpenOrders), method: HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<ParibuOrder>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<ParibuOrder>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data.Values.Select(x => x));
    }

    /// <summary>
    /// Place an Order
    /// </summary>
    /// <param name="symbol">Mandatory</param>
    /// <param name="side">Mandatory</param>
    /// <param name="type">Mandatory</param>
    /// <param name="total">Mandatory</param>
    /// <param name="amount">Mandatory for Limit Orders</param>
    /// <param name="price">Mandatory for Limit Orders</param>
    /// <param name="condition">Condition Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuOrder>> PlaceOrderAsync(
        string symbol,
        OrderSide side,
        OrderType type,
        decimal? total = null,
        decimal? amount = null,
        decimal? price = null,
        decimal? condition = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "market", symbol},
                { "trade", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
            };
        if (amount.HasValue) parameters.AddOptionalParameter("amount", amount.Value.ToString(CultureInfo.InvariantCulture));
        if (total.HasValue) parameters.AddOptionalParameter("total", amount.Value.ToString(CultureInfo.InvariantCulture));
        if (price.HasValue) parameters.AddOptionalParameter("price", price.Value.ToString(CultureInfo.InvariantCulture));
        if (condition.HasValue) parameters.AddOptionalParameter("condition", condition.Value.ToString(CultureInfo.InvariantCulture));

        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<ParibuOrder>>>(GetUri(Endpoints_Private_PlaceOrder), method: HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuOrder>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuOrder>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data.FirstOrDefault());
    }

    /// <summary>
    /// Cancels Order with specific Order Id
    /// </summary>
    /// <param name="orderId">Order Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>List of order Ids for canceled orders</returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> CancelOrderAsync(string orderId, CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<string>>>(GetUri(Endpoints_Private_CancelOrder.Replace("{uid}", orderId)), method: HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// symbol olarak market sembolü (btc-tl, usdt-tl gibi) iletilirse o marketteki tüm açık emirleri iptal eder. 
    /// symbol olarak "all" iletilirse tüm marketteki tüm açık emirleri iptal eder. 
    /// Cancels all open orders for a specific market symbol
    /// </summary>
    /// <param name="symbol">Market Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> CancelOrdersAsync(string symbol, CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<string>>>(GetUri(Endpoints_Private_CancelOrder.Replace("{uid}", symbol)), method: HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Gets User Initials
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuUserInitials>> GetUserInitialsAsync(CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuLoggedInInitials>>(GetUri(Endpoints_Public_Initials), method: HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuUserInitials>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuUserInitials>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data.UserInfo);
    }

    /// <summary>
    /// Gets Balances
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<Dictionary<string, ParibuAssetBalance>>> GetBalancesAsync(CancellationToken ct = default)
    {
        var result = await GetUserInitialsAsync(ct);
        if (!result.Success) return result.AsError<Dictionary<string, ParibuAssetBalance>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data == null) return result.AsError<Dictionary<string, ParibuAssetBalance>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.AssetBalances);

    }

    /// <summary>
    /// Gets Alerts
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<ParibuAlert>>> GetAlertsAsync(CancellationToken ct = default)
    {
        var result = await GetUserInitialsAsync(ct);
        if (!result.Success) return result.AsError<IEnumerable<ParibuAlert>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data == null) return result.AsError<IEnumerable<ParibuAlert>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Alerts);
    }

    /// <summary>
    /// Create new Price Alert
    /// </summary>
    /// <param name="symbol">Market Symbol</param>
    /// <param name="price">Alert Price</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<bool>> SetAlertAsync(string symbol, decimal price, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "market", symbol},
                { "trigger_price", price.ToString(CultureInfo.InvariantCulture)},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<object>>>(GetUri(Endpoints_Private_SetUserAlert), method: HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<bool>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<bool>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Success);
    }

    /// <summary>
    /// Cancels Price Alert with Alert Id
    /// </summary>
    /// <param name="alertId">Alert Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> CancelAlertAsync(string alertId, CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<string>>>(GetUri(Endpoints_Private_DeleteUserAlert.Replace("{uid}", alertId)), method: HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Cancels all Price Alerts with specific market symbol
    /// </summary>
    /// <param name="symbol">Market Symbol</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> CancelAlertsAsync(string symbol, CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<string>>>(GetUri(Endpoints_Private_DeleteUserAlert.Replace("{uid}", symbol)), method: HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Gets Deposit Addresses
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<ParibuAddress>>> GetDepositAddressesAsync(CancellationToken ct = default)
    {
        var result = await GetUserInitialsAsync(ct);
        if (!result.Success) return result.AsError<IEnumerable<ParibuAddress>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (result.Data == null) return result.AsError<IEnumerable<ParibuAddress>>(new ParibuRestApiError(null, string.Empty, null));

        var addresses = new List<ParibuAddress>();
        if (result.Data != null && result.Data.Addresses != null) addresses = result.Data.Addresses.Where(x => x.Direction == TransactionDirection.Deposit).ToList();

        return result.As(addresses.AsEnumerable());
    }

    /// <summary>
    /// Withdraw Method
    /// </summary>
    /// <param name="currency">Currency Symbol</param>
    /// <param name="amount">Withdrawal Amount</param>
    /// <param name="address">Withdrawal Address</param>
    /// <param name="tag">Withdrawal Tag</param>
    /// <param name="network">Withdrawal Network</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<ParibuTransaction>> WithdrawAsync(
        string currency,
        decimal amount,
        string address,
        string tag = "",
        string network = "",
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
            {
                { "currency", currency},
                { "amount", amount.ToString(CultureInfo.InvariantCulture)},
                { "address", address},
                { "tag", tag},
                { "network", network},
            };

        var result = await ExecuteAsync<ParibuRestApiResponse<ParibuTransaction>>(GetUri(Endpoints_Private_Withdraw), method: HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
        if (!result.Success) return result.AsError<ParibuTransaction>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<ParibuTransaction>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    /// <summary>
    /// Cancels Withdrawal Request
    /// </summary>
    /// <param name="withdrawalId">Withdrawal Id</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<RestCallResult<IEnumerable<string>>> CancelWithdrawalAsync(string withdrawalId, CancellationToken ct = default)
    {
        var result = await ExecuteAsync<ParibuRestApiResponse<IEnumerable<string>>>(GetUri(Endpoints_Private_CancelWithdrawal.Replace("{uid}", withdrawalId)), method: HttpMethod.Delete, ct, signed: true).ConfigureAwait(false);
        if (!result.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(result.Error.Code, result.Error.Message, result.Error.Data));
        if (!result.Data.Success) return result.AsError<IEnumerable<string>>(new ParibuRestApiError(null, string.Empty, null));

        return result.As(result.Data.Data);
    }

    #endregion

}