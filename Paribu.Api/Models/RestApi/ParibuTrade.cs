namespace Paribu.Api.Models.RestApi;

public class ParibuTrade
{
    [JsonProperty("timestamp"), JsonConverter(typeof(TimestampSecondsConverter))]
    public DateTime Timestamp { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("trade"), JsonConverter(typeof(OrderSideConverter))]
    public OrderSide Side { get; set; }
}
