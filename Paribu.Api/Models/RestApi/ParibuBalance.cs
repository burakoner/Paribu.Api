namespace Paribu.Api.Models.RestApi;

public record ParibuBalance
{
    [JsonProperty("currency")]
    public string Asset { get; set; } = "";

    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("blocked_in_orders")]
    public decimal BlockedInOrders { get; set; }

    [JsonProperty("blocked_in_transactions")]
    public decimal BlockedInTransfers { get; set; }

    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("details")]
    public ParibuBalanceDetails Details { get; set; } = default!;
}

public record ParibuBalanceDetails
{
    [JsonProperty("assigned")]
    public decimal? Assigned { get; set; }
}