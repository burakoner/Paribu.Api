namespace Paribu.Api.Models.RestApi;

public class ParibuTwoFactorToggle
{
    [JsonProperty("subject"), JsonConverter(typeof(TwoFactorSubjectConverter))]
    public TwoFactorSubject Subject { get; set; }
}

public class ParibuTwoFactorUser
{
    [JsonProperty("subject"), JsonConverter(typeof(TwoFactorSubjectConverter))]
    public TwoFactorSubject Subject { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }

    [JsonProperty("info")]
    public ParibuUserInfo UserInfo { get; set; }

    // Eğer açık emir yoksa boş array döndürüyor []
    // Eğer açık emir varsa dictionary döndürüyor {}
    // Bu nedenle bu kısmı şimdilik kapatıyorum.
    //[JsonProperty("openOrders")]
    //public Dictionary<string, ParibuOrder> OpenOrders { get; set; }

    [JsonProperty("balances")]
    public IEnumerable<ParibuTransaction> Transactions { get; set; }

    [JsonProperty("addresses")]
    public IEnumerable<ParibuAddress> Addresses { get; set; }

    [JsonProperty("alerts")]
    public IEnumerable<ParibuAlert> Alerts { get; set; }

    [JsonProperty("assets")]
    public Dictionary<string, ParibuAssetBalance> Assets { get; set; }

    [JsonProperty("logs")]
    public IEnumerable<ParibuLog> Logs { get; set; }

    [JsonProperty("limits")]
    public ParibuUserLimits Limits { get; set; }

    //[JsonProperty("bannerContent")]
    //public IEnumerable<ParibuBannerContent> BannerContents { get; set; }

    [JsonProperty("interests")]
    public IEnumerable<string> Interests { get; set; }

    //[JsonProperty("presaleOrders")]
    //public IEnumerable<ParibuPresaleOrder> PresaleOrders { get; set; }
}

public class ParibuUserInitials
{
    [JsonProperty("info")]
    public ParibuUserInfo UserInfo { get; set; }

    // Eğer açık emir yoksa boş array döndürüyor []
    // Eğer açık emir varsa dictionary döndürüyor {}
    // Bu nedenle bu kısmı şimdilik kapatıyorum.
    //[JsonProperty("openOrders")]
    //public Dictionary<string, ParibuOrder> OpenOrders { get; set; }

    [JsonProperty("balances")]
    public IEnumerable<ParibuTransaction> Transactions { get; set; }

    [JsonProperty("addresses")]
    public IEnumerable<ParibuAddress> Addresses { get; set; }

    [JsonProperty("alerts")]
    public IEnumerable<ParibuAlert> Alerts { get; set; }

    [JsonProperty("assets")]
    public Dictionary<string, ParibuAssetBalance> AssetBalances { get; set; }

    [JsonProperty("logs")]
    public IEnumerable<ParibuLog> Logs { get; set; }

    [JsonProperty("limits")]
    public ParibuUserLimits Limits { get; set; }

    //[JsonProperty("bannerContent")]
    //public IEnumerable<ParibuBannerContent> BannerContents { get; set; }

    [JsonProperty("interests")]
    public IEnumerable<string> Interests { get; set; }

    //[JsonProperty("presaleOrders")]
    //public IEnumerable<ParibuPresaleOrder> PresaleOrders { get; set; }
}

public class ParibuUserInfo
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("makerCommission")]
    public decimal MakerCommission { get; set; }

    [JsonProperty("takerCommission")]
    public decimal TakerCommission { get; set; }

    [JsonProperty("isAccountVerified")]
    public bool IsAccountVerified { get; set; }

    [JsonProperty("isEmailVerified")]
    public bool IsEmailVerified { get; set; }

    [JsonProperty("faMethod"), JsonConverter(typeof(TwoFactorMethodConverter))]
    public TwoFactorMethod TwoFactorMethod { get; set; }

    [JsonProperty("fiatDepositTag")]
    public string FiatDepositTag { get; set; }

    [JsonProperty("baseCurrency")]
    public string BaseCurrency { get; set; }

    [JsonProperty("interests")]
    public IEnumerable<ParibuBanner> Banners { get; set; }
}

public class ParibuAssetBalance
{
    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }
}

public class ParibuAddress
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(TransactionDirectionConverter))]
    public TransactionDirection Direction { get; set; }
}

public class ParibuAlert
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("market")]
    public string Symbol { get; set; }

    [JsonProperty("trigger")]
    public decimal Trigger { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(AlertDirectionConverter))]
    public AlertDirection Direction { get; set; }
}

public class ParibuLog
{
    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("details")]
    public string Details { get; set; }

    [JsonProperty("ip")]
    public string IpAddress { get; set; }

    [JsonProperty("os")]
    public string OS { get; set; }

    [JsonProperty("os_version")]
    public string OSVersion { get; set; }

    [JsonProperty("browser")]
    public string Browser { get; set; }

    [JsonProperty("browser_version")]
    public string BrowserVersion { get; set; }

    [JsonProperty("device")]
    public string Device { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
}

public class ParibuUserLimits
{
    [JsonProperty("daily")]
    public ParibuUserLimit Daily { get; set; }

    [JsonProperty("monthly")]
    public ParibuUserLimit Monthly { get; set; }
}

public class ParibuUserLimit
{
    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }
}
