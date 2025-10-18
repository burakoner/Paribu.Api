namespace Paribu.Api.Models.StreamApi;

public class ParibuStreamOrderBook
{
    public string Symbol { get; set; } = "";

    [JsonProperty("u")]
    public long UpdateId { get; set; }

    [JsonProperty("a")]
    public List<ParibuOrderBookEntry> Asks { get; set; } = [];

    [JsonProperty("b")]
    public List<ParibuOrderBookEntry> Bids { get; set; } = [];
}