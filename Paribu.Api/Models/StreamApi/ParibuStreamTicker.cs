namespace Paribu.Api.Models.StreamApi;

public class ParibuStreamTicker
{
    public string Symbol { get; set; } = "";

    [JsonProperty("l")]
    public decimal Low { get; set; }

    [JsonProperty("h")]
    public decimal High { get; set; }

    [JsonProperty("o")]
    public decimal First { get; set; }

    [JsonProperty("c")]
    public decimal Last { get; set; }

    [JsonProperty("v")]
    public decimal Volume { get; set; }

    [JsonProperty("q")]
    public decimal QuoteVolume { get; set; }

    [JsonProperty("p")]
    public decimal Change { get; set; }

    [JsonProperty("P")]
    public decimal Percentage { get; set; }

    [JsonProperty("w")]
    public decimal Average { get; set; }
}
