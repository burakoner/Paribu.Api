namespace Paribu.Api.Models.RestApi;

public record ParibuAppTrade
{
    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("trade")]
    public ParibuOrderSide TakerSide { get; set; }
}