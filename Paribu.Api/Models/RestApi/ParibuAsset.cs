namespace Paribu.Api.Models.RestApi;

public class ParibuAsset
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Icon { get; set; }

    public int Precision { get; set; }
    public decimal Step { get; set; }

    [JsonConverter(typeof(LabelConverter<ParibuAssetType>))]
    public ParibuAssetType Type { get; set; }

    [JsonProperty("hide_if_void")]
    public bool HideIfVoid { get; set; }

    public IEnumerable<string> Labels { get; set; }
    public IEnumerable<string> Networks { get; set; }

    [JsonProperty("deposit_limits")]
    public ParibuAssetDepositLimits DepositLimits { get; set; }

    [JsonProperty("withdraw_limits")]
    public ParibuAssetWithdrawalLimits WithdrawLimits { get; set; }
}

public class ParibuAssetDepositLimits
{
    [JsonProperty("min_amount")]
    public decimal MinimumAmount { get; set; }
}

public class ParibuAssetWithdrawalLimits
{
    [JsonProperty("min_once")]
    public decimal OnceMinimum { get; set; }

    [JsonProperty("max_once")]
    public decimal OnceMaximum { get; set; }

    [JsonProperty("max_daily")]
    public decimal DailyMaximum { get; set; }

    [JsonProperty("max_monthly")]
    public decimal MonthlyMaximum { get; set; }
}