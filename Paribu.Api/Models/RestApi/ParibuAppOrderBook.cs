namespace Paribu.Api.Models.RestApi;

public record ParibuAppOrderBook
{
    [JsonProperty("buy")]
    private Dictionary<decimal, decimal> Buys { get; set; } = [];
    public List<ParibuAppOrderBookEntry> Bids
    {
        get
        {
            return [.. Buys.Select(item => new ParibuAppOrderBookEntry
            {
                Price = item.Key,
                Amount = item.Value,
            }).OrderByDescending(x=>x.Price)];
        }
    }

    [JsonProperty("sell")]
    private Dictionary<decimal, decimal> Sells { get; set; } = [];
    public List<ParibuAppOrderBookEntry> Asks
    {
        get
        {
            return [.. Sells.Select(item => new ParibuAppOrderBookEntry
            {
                Price = item.Key,
                Amount = item.Value,
            }).OrderBy(x=>x.Price)];
        }
    }
}

public record ParibuAppOrderBookEntry
{
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
