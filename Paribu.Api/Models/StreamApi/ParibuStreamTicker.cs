namespace Paribu.Api.Models.StreamApi;

/// <summary>
/// Caution: Not all fields come all the time. Check always if value is null.
/// </summary>
public class ParibuStreamTicker
{
    public string Symbol { get; set; }

    [JsonProperty("o")]
    public decimal? Open { get; set; }

    [JsonProperty("h")]
    public decimal? High { get; set; }

    [JsonProperty("l")]
    public decimal? Low { get; set; }

    [JsonProperty("c")]
    public decimal? Close { get; set; }

    [JsonProperty("v")]
    public decimal? Volume { get; set; }

    [JsonProperty("ch")]
    public decimal? Change { get; set; }

    [JsonProperty("p")]
    public decimal? ChangePercent { get; set; }

    [JsonProperty("a")]
    public decimal? Average24H { get; set; }

    [JsonProperty("g")]
    public decimal? VolumeQuote { get; set; }

    [JsonProperty("b")]
    public decimal? Bid { get; set; }

    [JsonProperty("s")]
    public decimal? Ask { get; set; }

    [JsonProperty("es")]
    public decimal? EAsk { get; set; }

    [JsonProperty("eb")]
    public decimal? EBid { get; set; }

    [JsonProperty("priceSeries")]
    public IEnumerable<decimal> PriceSeries { get; set; }
}

public class ParibuStreamPriceSeries
{
    public string Symbol { get; set; }

    public IEnumerable<decimal> Prices { get; set; }
}