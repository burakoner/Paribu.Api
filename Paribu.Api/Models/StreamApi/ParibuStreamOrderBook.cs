namespace Paribu.Api.Models.StreamApi;

public class ParibuStreamOrderBook
{
    public string Symbol { get; set; }

    public List<ParibuStreamOrderBookEntry> BidsToAdd { get; set; }
    public List<ParibuStreamOrderBookEntry> BidsToRemove { get; set; }

    public List<ParibuStreamOrderBookEntry> AsksToAdd { get; set; }
    public List<ParibuStreamOrderBookEntry> AsksToRemove { get; set; }

    public ParibuStreamOrderBook()
    {
        BidsToAdd = new List<ParibuStreamOrderBookEntry>();
        BidsToRemove = new List<ParibuStreamOrderBookEntry>();
        AsksToAdd = new List<ParibuStreamOrderBookEntry>();
        AsksToRemove = new List<ParibuStreamOrderBookEntry>();
    }
}

public class ParibuStreamOrderBookEntry
{
    public decimal Amount { get; set; }

    public decimal Price { get; set; }
}