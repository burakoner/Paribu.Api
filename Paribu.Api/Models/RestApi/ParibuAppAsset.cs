namespace Paribu.Api.Models.RestApi;

public record ParibuAppAsset
{
    public string Symbol { get; set; } = "";

    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Color { get; set; } = "";
    public ParibuAssetType Type { get; set; }

    public decimal Step { get; set; }
    public int Precision { get; set; }

    [JsonProperty("hide_if_void")]
    public bool HideIfVoid { get; set; }

    public List<string> Labels { get; set; } = [];
    public List<string> Networks { get; set; } = [];

    [JsonProperty("listing_date")]
    public DateTime? ListingDate { get; set; }

    [JsonProperty("deposit_limits")]
    public ParibuAppAssetDepositLimits DepositLimits { get; set; } = default!;

    [JsonProperty("withdraw_limits")]
    public ParibuAppAssetWithdrawalLimits WithdrawLimits { get; set; } = default!;

    internal ParibuAppAsset SetSymbol(string? symbol = null)
    {
        if (symbol != null) Symbol = symbol;
        return this;
    }
}

public record ParibuAppAssetDepositLimits
{
    [JsonProperty("min_amount")]
    public decimal MinimumAmount { get; set; }
}

public record ParibuAppAssetWithdrawalLimits
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