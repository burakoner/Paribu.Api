namespace Paribu.Api.Converters;

public class CurrencyGroupConverter : BaseConverter<CurrencyGroup>
{
    public CurrencyGroupConverter() : this(true) { }
    public CurrencyGroupConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CurrencyGroup, string>> Mapping => new List<KeyValuePair<CurrencyGroup, string>>
    {
        new KeyValuePair<CurrencyGroup, string>(CurrencyGroup.Fiat, "fiat"),
        new KeyValuePair<CurrencyGroup, string>(CurrencyGroup.Crypto, "crypto"),
        new KeyValuePair<CurrencyGroup, string>(CurrencyGroup.FanToken, "fantoken"),
    };
}