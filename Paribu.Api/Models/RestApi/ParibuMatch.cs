namespace Paribu.Api.Models.RestApi;

public class ParibuMatch
{
    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("trade"), JsonConverter(typeof(LabelConverter<ParibuOrderSide>))]
    public ParibuOrderSide Side { get; set; }
}