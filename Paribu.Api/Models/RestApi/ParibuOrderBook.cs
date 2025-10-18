namespace Paribu.Api.Models.RestApi;

public record ParibuOrderBook
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("asks")]
    public List<ParibuOrderBookEntry> Asks { get; set; } = [];

    [JsonProperty("bids")]
    public List<ParibuOrderBookEntry> Bids { get; set; } = [];
}

[JsonConverter(typeof(ArrayConverter))]
public record ParibuOrderBookEntry
{
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    [ArrayProperty(1)]
    public decimal Amount { get; set; }
}
