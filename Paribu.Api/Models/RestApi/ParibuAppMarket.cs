namespace Paribu.Api.Models.RestApi;

public record ParibuAppMarket
{
    public string Symbol { get; set; } = "";

    public List<string> Labels { get; set; } = [];

    [JsonProperty("pairs")]
    internal ParibuAppMarketPairs Pairs { get; set; } = default!;
    public string BaseAsset => Pairs.BaseAsset;
    public string QuoteAsset => Pairs.QuoteAsset;

    [JsonProperty("precisions")]
    internal ParibuAppMarketPrecisions Precisions { get; set; } = default!;
    public int AmountPrecision => Precisions.AmountPrecision;
    public int PricePrecision => Precisions.PricePrecision;

    [JsonProperty("steps")]
    internal ParibuAppMarketSteps Steps { get; set; } = default!;
    public decimal AmountStep => Steps.AmountStep;
    public decimal PriceStep => Steps.PriceStep;

    internal ParibuAppMarket SetSymbol(string? symbol = null)
    {
        if (symbol != null) Symbol = symbol;
        return this;
    }
}

public record ParibuAppMarketPairs
{
    [JsonProperty("market")]
    public string BaseAsset { get; set; } = "";

    [JsonProperty("payment")]
    public string QuoteAsset { get; set; } = "";
}

public record ParibuAppMarketPrecisions
{
    [JsonProperty("amount")]
    public int AmountPrecision { get; set; }

    [JsonProperty("price")]
    public int PricePrecision { get; set; }
}

public record ParibuAppMarketSteps
{
    [JsonProperty("amount")]
    public decimal AmountStep { get; set; }

    [JsonProperty("price")]
    public decimal PriceStep { get; set; }
}