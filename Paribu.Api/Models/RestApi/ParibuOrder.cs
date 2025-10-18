namespace Paribu.Api.Models.RestApi;

public record ParibuOrder
{
    [JsonProperty("uid")]
    public string OrderId { get; set; } = "";

    [JsonProperty("user_uid")]
    public string UserId { get; set; } = "";

    [JsonProperty("market")]
    public string Symbol { get; set; } = "";

    [JsonProperty("trade")]
    public ParibuOrderSide Side { get; set; }

    [JsonProperty("type")]
    public ParibuOrderType Type { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("remaining_amount")]
    public decimal RemainingAmount { get; set; }

    [JsonProperty("average")]
    public decimal AveragePrice { get; set; }

    [JsonProperty("condition")]
    public decimal? Condition { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = ""; // open, close

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("opened_at")]
    public DateTime? OpenedAt { get; set; }
    
    [JsonProperty("closed_at")]
    public DateTime? ClosedAt { get; set; }
}