namespace Paribu.Api.Converters;

public class MarketGroupConverter : BaseConverter<MarketGroup>
{
    public MarketGroupConverter() : this(true) { }
    public MarketGroupConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarketGroup, string>> Mapping => new List<KeyValuePair<MarketGroup, string>>
    {
        new KeyValuePair<MarketGroup, string>(MarketGroup.CryptoTL, "crypto-tl"),
        new KeyValuePair<MarketGroup, string>(MarketGroup.CryptoUSDT, "crypto-usdt"),
        new KeyValuePair<MarketGroup, string>(MarketGroup.FanTokenCHZ, "fantoken-chz"),
    };
}