namespace Paribu.Api.Models.RestApi;

[JsonConverter(typeof(TypedDataConverter<ParibuTickers>))]
public class ParibuTickers
{
    [TypedData]
    public Dictionary<string, ParibuTicker> Data { get; set; }
}

public class ParibuTicker
{
    [JsonProperty("pair")]
    public string Symbol { get; set; }

    [JsonProperty("high")]
    public decimal High { get; set; }

    [JsonProperty("low")]
    public decimal Low { get; set; }

    [JsonProperty("bid")]
    public decimal Bid { get; set; }

    [JsonProperty("ask")]
    public decimal Ask { get; set; }

    [JsonProperty("open")]
    public decimal Open { get; set; }

    [JsonProperty("close")]
    public decimal Close { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("average")]
    public decimal Average { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }
}
