namespace Paribu.Api.Models.RestApi;

public class ParibuOrderBook
{
    [JsonProperty("buy")]
    private Dictionary<decimal, decimal> Buys { get; set; }
    public IEnumerable<ParibuOrderBookEntry> Bids 
    { 
        get
        {
            var bids = new List<ParibuOrderBookEntry>();
            foreach (var item in Buys)
            {
                bids.Add(new ParibuOrderBookEntry
                {
                    Price = item.Key,
                    Amount = item.Value,
                });
            }
            return bids;
        }
    }

    [JsonProperty("sell")]
    private Dictionary<decimal, decimal> Sells { get; set; }
    public IEnumerable<ParibuOrderBookEntry> Asks
    {
        get
        {
            var bids = new List<ParibuOrderBookEntry>();
            foreach (var item in Sells)
            {
                bids.Add(new ParibuOrderBookEntry
                {
                    Price = item.Key,
                    Amount = item.Value,
                });
            }
            return bids;
        }
    }
}

public class ParibuOrderBookEntry
{
    public decimal Amount { get; set; }

    public decimal Price { get; set; }
}
