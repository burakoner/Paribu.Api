namespace Paribu.Api.Models.RestApi;

public class ParibuInitials
{
    [JsonProperty("bannerContent")]
    public IEnumerable<ParibuBanner> Banners { get; set; }

    [JsonProperty("displayGroups")]
    public ParibuDisplayGroups DisplayGroups { get; set; }

    [JsonProperty("config")]
    public ParibuExchangeConfig ExchangeConfig { get; set; }

    [JsonProperty("currencies")]
    private ParibuInitialsCurrencies CurrenciesContainer { get; set; }

    [JsonProperty("markets")]
    private ParibuInitialsMarkets MarketsContainer { get; set; }

    [JsonProperty("ticker")]
    private ParibuInitialTickers TickersContainer { get; set; }

    [JsonIgnore]
    public Dictionary<string, ParibuCurrency> Currencies { get { return CurrenciesContainer?.Data; } }

    [JsonIgnore]
    public Dictionary<string, ParibuMarket> Markets { get { return MarketsContainer?.Data; } }

    [JsonIgnore]
    public Dictionary<string, ParibuInitialTicker> Tickers { get { return TickersContainer?.Data; } }
}

public class ParibuLoggedInInitials : ParibuInitials
{
    [JsonProperty("user")]
    public ParibuUserInitials UserInfo { get; set; }
}

public class ParibuExchangeConfig
{
    [JsonProperty("easyMarkets")]
    private ParibuEasyMarketLimits EasyMarketsLimitsContainer { get; set; }

    [JsonProperty("bankAccounts")]
    private ParibuBankAccounts BankAccountsContainer { get; set; }

    [JsonProperty("papara")]
    public ParibuPapara Papara { get; set; }

    [JsonProperty("ininal")]
    public ParibuIninal Ininal { get; set; }

    [JsonProperty("networkFees")]
    private ParibuNetworkFees NetworkFeesContainer { get; set; }

    [JsonProperty("appVersions")]
    public ParibuAppVersion AppVersions { get; set; }

    [JsonIgnore]
    public Dictionary<string, ParibuLimit> EasyMarketLimits { get { return EasyMarketsLimitsContainer?.Data; } }

    [JsonIgnore]
    public Dictionary<string, IEnumerable<ParibuBankAccount>> BankAccounts { get { return BankAccountsContainer?.Data; } }

    [JsonIgnore]
    public Dictionary<string, Dictionary<string, decimal>> NetworkFees { get { return NetworkFeesContainer?.Data; } }
}

[JsonConverter(typeof(TypedDataConverter<ParibuEasyMarketLimits>))]
public class ParibuEasyMarketLimits
{
    [TypedData]
    public Dictionary<string, ParibuLimit> Data { get; set; }
}

public class ParibuLimit
{
    [JsonProperty("limit")]
    public decimal Limit { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuBankAccounts>))]
public class ParibuBankAccounts
{
    [TypedData]
    public Dictionary<string, IEnumerable<ParibuBankAccount>> Data { get; set; }
}

public class ParibuBankAccount
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("iban")]
    public string IBAN { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("sorter")]
    public int Sorter { get; set; }
}

public class ParibuPapara
{
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("maxWithdraw")]
    public decimal MaxWithdraw { get; set; }

    [JsonProperty("minWithdraw")]
    public decimal MinWithdraw { get; set; }

    [JsonProperty("maxDeposit")]
    public decimal MaxDeposit { get; set; }

    [JsonProperty("minDeposit")]
    public decimal MinDeposit { get; set; }

    [JsonProperty("feePercent")]
    public decimal FeePercent { get; set; }

    [JsonProperty("taxPercent")]
    public decimal TaxPercent { get; set; }

    [JsonProperty("feeMaxCap")]
    public decimal FeeMaxCap { get; set; }
}

public class ParibuIninal
{
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuNetworkFees>))]
public class ParibuNetworkFees
{
    [TypedData]
    public Dictionary<string, Dictionary<string, decimal>> Data { get; set; }
}

public class ParibuAppVersion
{
    [JsonProperty("latest")]
    public int Latest { get; set; }

    [JsonProperty("minRequired")]
    public int MinRequired { get; set; }
}

public class ParibuBanner
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("buttonText")]
    public string ButtonText { get; set; }

    [JsonProperty("buttonHref")]
    public string ButtonHref { get; set; }

    [JsonProperty("navigateTo")]
    public ParibuBannerNavigate NavigateTo { get; set; }
}

public class ParibuBannerNavigate
{
    [JsonProperty("target")]
    public string Target { get; set; }

    [JsonProperty("arguments")]
    public ParibuBannerNavigateArguments Arguments { get; set; }
}

public class ParibuBannerNavigateArguments
{
    [JsonProperty("screen")]
    public string Screen { get; set; }

    [JsonProperty("params")]
    public ParibuBannerNavigateArgumentsParams Params { get; set; }
}

public class ParibuBannerNavigateArgumentsParams
{
    [JsonProperty("marketKey")]
    public string MarketKey { get; set; }
}

public class ParibuDisplayGroups
{
    [JsonProperty("marketGroups")]
    public ParibuDisplayMarketGroups MarketGroups { get; set; }

    [JsonProperty("currencyGroups")]
    public ParibuDisplayCurrencyGroups CurrencyGroups { get; set; }
}

public class ParibuDisplayMarketGroups
{
    [JsonProperty("crypto-tl")]
    public ParibuDisplayMarketGroup CryptoTL { get; set; }

    [JsonProperty("crypto-usdt")]
    public ParibuDisplayMarketGroup CryptoUSDT { get; set; }

    [JsonProperty("fantoken-chz")]
    public ParibuDisplayMarketGroup FanTokenCHZ { get; set; }
}

public class ParibuDisplayMarketGroup
{
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("tab")]
    public string Tab { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("visible")]
    public bool Visible { get; set; }

    [JsonProperty("sorter")]
    public int Sorter { get; set; }
}

public class ParibuDisplayCurrencyGroups
{
    [JsonProperty("fiat")]
    public ParibuDisplayCurrencyGroup Fiat { get; set; }

    [JsonProperty("crypto")]
    public ParibuDisplayCurrencyGroup Crypto { get; set; }

    [JsonProperty("fantoken")]
    public ParibuDisplayCurrencyGroup FanToken { get; set; }
}

public class ParibuDisplayCurrencyGroup
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("visible")]
    public bool Visible { get; set; }

    [JsonProperty("sorter")]
    public int Sorter { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuInitialsCurrencies>))]
public class ParibuInitialsCurrencies
{
    [TypedData]
    public Dictionary<string, ParibuCurrency> Data { get; set; }
}

public class ParibuCurrency
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(CurrencyTypeConverter))]
    public CurrencyType Type { get; set; }

    [JsonProperty("group"), JsonConverter(typeof(CurrencyGroupConverter))]
    public CurrencyGroup Group { get; set; }

    [JsonProperty("color")]
    public string Color { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("displayDecimals")]
    public int DisplayDecimals { get; set; }

    [JsonProperty("deposit")]
    public ParibuCurrencyDeposit DepositOptions { get; set; }

    [JsonProperty("withdraw")]
    public ParibuCurrencyWithdraw WithdrawOptions { get; set; }

    [JsonProperty("networks")]
    private ParibuCurrencyNetwork NetworksContainer { get; set; }

    [JsonIgnore]
    public Dictionary<string, string> Networks { get { return NetworksContainer?.Data; } }
}

[JsonConverter(typeof(TypedDataConverter<ParibuCurrencyNetwork>))]
public class ParibuCurrencyNetwork
{
    [TypedData]
    public Dictionary<string, string> Data { get; set; }
}

public class ParibuCurrencyDeposit
{
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("addressesAllowed")]
    public int AddressesAllowed { get; set; }

    [JsonProperty("confirmationsRequired")]
    public int ConfirmationsRequired { get; set; }
}

public class ParibuCurrencyWithdraw
{
    [JsonProperty("enabled")]
    public bool Enabled { get; set; }

    [JsonProperty("minAmount")]
    public decimal MinAmount { get; set; }

    [JsonProperty("maxAmount")]
    public decimal MaxAmount { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("decimals")]
    public int Decimals { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuInitialsMarkets>))]
public class ParibuInitialsMarkets
{
    [TypedData]
    public Dictionary<string, ParibuMarket> Data { get; set; }
}

public class ParibuMarket
{
    [JsonProperty("pairs")]
    public string Symbol { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("minAmount")]
    public decimal MinAmount { get; set; }

    [JsonProperty("maxAmount")]
    public decimal MaxAmount { get; set; }

    [JsonProperty("mDecimals")]
    public int SizeDecimals { get; set; }

    [JsonProperty("pDecimals")]
    public int PriceDecimals { get; set; }

    [JsonProperty("group"), JsonConverter(typeof(MarketGroupConverter))]
    public MarketGroup Group { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuInitialTickers>))]
public class ParibuInitialTickers
{
    [TypedData]
    public Dictionary<string, ParibuInitialTicker> Data { get; set; }
}

public class ParibuInitialTicker
{
    [JsonProperty("o")]
    public decimal Open { get; set; }

    [JsonProperty("h")]
    public decimal High { get; set; }

    [JsonProperty("l")]
    public decimal Low { get; set; }

    [JsonProperty("c")]
    public decimal Close { get; set; }

    [JsonProperty("v")]
    public decimal Volume { get; set; }

    [JsonProperty("ch")]
    public decimal Change { get; set; }

    [JsonProperty("p")]
    public decimal ChangePercent { get; set; }

    [JsonProperty("a")]
    public decimal Average24H { get; set; }

    [JsonProperty("g")]
    public decimal VolumeQuote { get; set; }

    [JsonProperty("b")]
    public decimal Buy { get; set; }

    [JsonProperty("s")]
    public decimal Sell { get; set; }

    [JsonProperty("es")]
    public decimal ESell { get; set; }

    [JsonProperty("eb")]
    public decimal EBuy { get; set; }

    [JsonProperty("priceSeries")]
    public IEnumerable<decimal> PriceSeries { get; set; }
}
