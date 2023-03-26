namespace Paribu.Api.Converters;

public class TwoFactorMethodConverter : BaseConverter<TwoFactorMethod>
{
    public TwoFactorMethodConverter() : this(true) { }
    public TwoFactorMethodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<TwoFactorMethod, string>> Mapping => new List<KeyValuePair<TwoFactorMethod, string>>
    {
        new KeyValuePair<TwoFactorMethod, string>(TwoFactorMethod.SMS, "sms"),
        new KeyValuePair<TwoFactorMethod, string>(TwoFactorMethod.GoogleAuthenticator, "g2fa"),
    };
}