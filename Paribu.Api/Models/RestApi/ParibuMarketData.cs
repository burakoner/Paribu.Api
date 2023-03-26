namespace Paribu.Api.Models.RestApi;

public class ParibuMarketData
{
    public ParibuOrderBook OrderBook { get; set; }

    public IEnumerable<ParibuMarketMatch> MarketMatches { get; set; }

    public IEnumerable<ParibuUserMatch> UserMatches { get; set; }

    public ParibuChartData ChartData { get; set; }
}

public class ParibuOrderBook
{
    public List<ParibuOrderBookEntry> Bids { get; set; }

    public List<ParibuOrderBookEntry> Asks { get; set; }

    public ParibuOrderBook()
    {
        Bids = new List<ParibuOrderBookEntry>();
        Asks = new List<ParibuOrderBookEntry>();
    }
}

public class ParibuOrderBookEntry
{
    public decimal Amount { get; set; }

    public decimal Price { get; set; }
}

internal class MarketData
{
    [JsonProperty("orderBook")]
    public MarketDataOrderBook OrderBook { get; set; }

    [JsonProperty("marketMatches")]
    public IEnumerable<ParibuMarketMatch> MarketMatches { get; set; }

    [JsonProperty("userMatches")]
    public IEnumerable<ParibuUserMatch> UserMatches { get; set; }

    [JsonProperty("charts")]
    public ChartData ChartData { get; set; }
}

internal class MarketDataOrderBook
{
    [JsonProperty("buy")]
    public MarketDataOrderBookList Bids { get; set; }

    [JsonProperty("sell")]
    public MarketDataOrderBookList Asks { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<MarketDataOrderBookList>))]
internal class MarketDataOrderBookList
{
    [TypedData]
    public Dictionary<decimal, decimal> Data { get; set; }
}

public class ParibuMarketMatch : ParibuTrade
{
}

public class ParibuUserMatch : ParibuTrade
{
    [JsonProperty("ratio")]
    public decimal Ratio { get; set; }

    [JsonProperty("commission")]
    public decimal Commission { get; set; }

    [JsonProperty("role"), JsonConverter(typeof(TradeRoleConverter))]
    public TradeRole Role { get; set; }

}