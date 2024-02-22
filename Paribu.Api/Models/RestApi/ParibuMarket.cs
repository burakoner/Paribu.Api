namespace Paribu.Api.Models.RestApi;

public class ParibuMarket
{
    public IEnumerable<string> Labels { get; set; }

    [JsonProperty("pairs")]
    public ParibuMarketPairs Pairs { get; set; }

    [JsonProperty("precisions")]
    public ParibuMarketPrecisions Precisions { get; set; }

    [JsonProperty("steps")]
    public ParibuMarketSteps Steps { get; set; }
}

public class ParibuMarketPairs
{
    [JsonProperty("market")]
    public string BaseAsset { get; set; }

    [JsonProperty("payment")]
    public string QuoteAsset { get; set; }
}

public class ParibuMarketPrecisions
{
    [JsonProperty("amount")]
    public int AmountPrecision { get; set; }

    [JsonProperty("price")]
    public int PricePrecision { get; set; }
}

public class ParibuMarketSteps
{
    [JsonProperty("amount")]
    public decimal AmountStep { get; set; }

    [JsonProperty("price")]
    public decimal PriceStep { get; set; }
}