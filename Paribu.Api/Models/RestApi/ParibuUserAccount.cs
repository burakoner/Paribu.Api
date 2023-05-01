namespace Paribu.Api.Models.RestApi;

public class ParibuUserAccount
{
    [JsonProperty("info")]
    public ParibuUserInfo Information { get; set; }

    [JsonProperty("addresses")]
    public Dictionary<string, ParibuSavedAddress> Addresses { get; set; }

    [JsonProperty("assets")]
    public Dictionary<string, ParibuAssetBalance> Balances { get; set; }

    [JsonProperty("favorites")]
    public IEnumerable<string> Favorites { get; set; }

    [JsonProperty("alarms")]
    public Dictionary<string, ParibuAlarm> Alarms { get; set; }

    [JsonProperty("open_orders")]
    public Dictionary<string, ParibuOrder> OpenOrders { get; set; }

    //[JsonProperty("config")]
    //public config Configuration { get; set; }
}

public class ParibuUserInfo
{
    public string UID { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Name { get; set; }

    [JsonProperty("security")]
    public ParibuUserSecurity Security { get; set; }

    [JsonProperty("settings")]
    public ParibuUserSettings Settings { get; set; }

    [JsonProperty("todo_list")]
    public IEnumerable<ParibuUserToDo> ToDoList { get; set; }

    [JsonProperty("trade")]
    public ParibuUserTraderProfile TraderProfile { get; set; }
}

public class ParibuUserSecurity
{
    [JsonProperty("g2fa")]
    public bool Google2FA{ get; set; }
    
    [JsonProperty("is_account_verified")]
    public bool IsAccountVerified { get; set; }
    
    [JsonProperty("is_email_verified")]
    public bool IsEmailVerified { get; set; }
    
    [JsonProperty("is_identity_verified")]
    public bool IsIdentityVerified { get; set; }
    
    [JsonProperty("identity_missing_fields")]
    public IEnumerable< string> MissingIdentityFields { get; set; }

    [JsonProperty("nationality")]
    public string Nationality { get; set; }
}

public class ParibuUserSettings
{
    [JsonProperty("base_currency")]
    public string BaseCurrency { get; set; }

    [JsonProperty("gaid")]
    public string GAID { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }
}

public class ParibuUserTraderProfile
{
    [JsonProperty("commissions")]
    public ParibuUserCommissions Commissions { get; set; }

    [JsonProperty("volume")]
    public ParibuUserVolume Volume { get; set; }
}

public class ParibuUserCommissions
{
    public decimal Maker { get; set; }
    public decimal Taker { get; set; }
}

public class ParibuUserVolume
{
    [JsonProperty("monthly")]
    public decimal MonthlyVolume { get; set; }
}

public class ParibuUserToDo
{
    public ParibuLanguageResource Title { get; set; }
    public ParibuLanguageResource Description { get; set; }
    public string Icon { get; set; }
    public string Link { get; set; }
    public int Sorter { get; set; }
}

public class ParibuAssetBalance
{
    public decimal Available { get; set; }
    public decimal Locked { get; set; }
    public decimal Total { get; set; }

    [JsonProperty("blocked_in_orders")]
    public decimal BlockedInOrders { get; set; }

    [JsonProperty("blocked_in_transactions")]
    public decimal BlockedInTransactions { get; set; }

    [JsonProperty("details")]
    public ParibuAssetBalanceDetails Details { get; set; }
}

public class ParibuAssetBalanceDetails
{
    [JsonProperty("match_buy_commission")]
    public decimal MatchBuyCommission { get; set; }

    [JsonProperty("match_buy_in")]
    public decimal MatchBuyIn { get; set; }
    
    [JsonProperty("match_sell_out")]
    public decimal MatchSellOut { get; set; }
    
    [JsonProperty("transfer_deposit")]
    public decimal TransferDeposit { get; set; }
    
    [JsonProperty("transfer_withdraw")]
    public decimal TransferWithdraw { get; set; }
    
    [JsonProperty("transfer_withdraw_fee")]
    public decimal TransferWithdrawFee { get; set; }
}

public class ParibuSavedAddress
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("user_uid")]
    public string UserId { get; set; }

    public string Label { get; set; }
    public string Address { get; set; }
    public string Tag { get; set; }

    [JsonProperty("currency")]
    public string Asset { get; set; }

    [JsonProperty("network")]
    public string Network { get; set; }

    [JsonProperty("network_type")]
    public string NetworkType { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(LabelConverter<ParibuTransferDirection>))]
    public ParibuTransferDirection Direction { get; set; }

    [JsonProperty("bank")]
    public ParibuBankDetails Bank { get; set; }
}

public class ParibuBankDetails
{
    [JsonProperty("bank_id")]
    public string BankId { get; set; }

    public string Icon { get; set; }
    public string Name { get; set; }
}