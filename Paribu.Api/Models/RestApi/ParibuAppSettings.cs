namespace Paribu.Api.Models.RestApi;

public class ParibuAppSettings
{
    [JsonProperty("bank_accounts")]
    public Dictionary<string, IEnumerable<ParibuBankAccount>> BankAccounts { get; set; }

    [JsonProperty("commission_rates")]
    public IEnumerable<ParibuCommissionRate> CommissionRates { get; set; }

    [JsonProperty("currency_groups")]
    public IEnumerable<ParibuCurrencyGroup> CurrencyGroups { get; set; }

    [JsonProperty("locks")]
    public ParibuLockDurations LockDurations { get; set; }

    [JsonProperty("market_groups")]
    public IEnumerable<ParibuMarketGroup> MarketGroups { get; set; }

    [JsonProperty("throttles")]
    public ParibuThrottles Throttles { get; set; }

    [JsonProperty("unverified_user_commission_rate")]
    public ParibuUnverifiedCommissionRate UnverifiedCommissionRate { get; set; }

    [JsonProperty("version")]
    public ParibuVersion Version { get; set; }
}

public class ParibuBankAccount
{
    public string Currency { get; set; }
    public string IBAN { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    public int Sorter { get; set; }
}

public class ParibuCommissionRate
{
    public int Level { get; set; }
    public decimal Maker { get; set; }
    public decimal Taker { get; set; }

    [JsonProperty("min")]
    public decimal? Minimum { get; set; }

    [JsonProperty("max")]
    public decimal? Maximum { get; set; }
}

public class ParibuUnverifiedCommissionRate
{
    public decimal Maker { get; set; }
    public decimal Taker { get; set; }
}

public class ParibuCurrencyGroup
{
    public string Key { get; set; }
}

public class ParibuMarketGroup
{
    public string Key { get; set; }
    public bool Featured { get; set; }
}

public class ParibuLockDurations
{
    [JsonProperty("email_changed")]
    public int EmailChanged { get; set; }

    [JsonProperty("financial_operation")]
    public int FinancialOperation { get; set; }

    [JsonProperty("first_deposit")]
    public int FirstDeposit { get; set; }

    [JsonProperty("password_reset")]
    public int PasswordReset { get; set; }
}

public class ParibuThrottles
{
    [JsonProperty("check_password")]
    public ParibuThrottle CheckPassword { get; set; }

    [JsonProperty("mfa_email")]
    public ParibuThrottle MfaEmail { get; set; }

    [JsonProperty("mfa_sms")]
    public ParibuThrottle MfaSms { get; set; }
}

public class ParibuThrottle
{
    [JsonProperty("decay_seconds")]
    public int DecaySeconds { get; set; }

    [JsonProperty("max_attempts")]
    public int MaxAttempts { get; set; }
}

public class ParibuVersion
{
    [JsonProperty("min_version")]
    public string MinimumVersion { get; set; }

    [JsonProperty("recommended_version")]
    public string RecommendedVersion { get; set; }
}
