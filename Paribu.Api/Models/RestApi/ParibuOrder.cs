namespace Paribu.Api.Models.RestApi;

public class ParibuOrder
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("user_uid")]
    public string UserId { get; set; }

    [JsonProperty("market")]
    public string Symbol { get; set; }

    public decimal Amount { get; set; }
    public decimal Average { get; set; }
    public decimal? Price { get; set; }

    public string Status { get; set; } // open, close

    [JsonProperty("trade"), JsonConverter(typeof(LabelConverter<ParibuOrderSide>))]
    public ParibuOrderSide Side { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(LabelConverter<ParibuOrderType>))]
    public ParibuOrderType Type { get; set; }

    [JsonProperty("remaining_amount")]
    public decimal RemainingAmount { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("opened_at")]
    public DateTime? OpenedAt { get; set; }
    
    [JsonProperty("closed_at")]
    public DateTime? ClosedAt { get; set; }

    public decimal? Condition { get; set; }
}