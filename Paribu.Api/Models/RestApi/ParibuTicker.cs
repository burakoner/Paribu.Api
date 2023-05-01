namespace Paribu.Api.Models.RestApi;

public class ParibuTicker
{
    [JsonProperty("id")]
    public string Symbol { get; set; }

    [JsonProperty("interval")]
    public string Interval { get; set; }

    [JsonProperty("lowest")]
    public decimal Lowest { get; set; }

    [JsonProperty("highest")]
    public decimal Highest { get; set; }

    [JsonProperty("first")]
    public decimal First { get; set; }

    [JsonProperty("last")]
    public decimal Last { get; set; }

    [JsonProperty("volume")]
    public decimal Volume { get; set; }

    [JsonProperty("pair_volume")]
    public decimal QuoteVolume { get; set; }

    [JsonProperty("change")]
    public decimal Change { get; set; }

    [JsonProperty("percentage")]
    public decimal Percentage { get; set; }

    [JsonProperty("percentage1h")]
    public decimal Percentage1H { get; set; }

    [JsonProperty("percentage4h")]
    public decimal Percentage4H { get; set; }

    [JsonProperty("average")]
    public decimal Average { get; set; }
}