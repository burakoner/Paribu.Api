namespace Paribu.Api.Converters;

public class TwoFactorSubjectConverter : BaseConverter<TwoFactorSubject>
{
    public TwoFactorSubjectConverter() : this(true) { }
    public TwoFactorSubjectConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<TwoFactorSubject, string>> Mapping => new List<KeyValuePair<TwoFactorSubject, string>>
    {
        new KeyValuePair<TwoFactorSubject, string>(TwoFactorSubject.Login, "login"),
        new KeyValuePair<TwoFactorSubject, string>(TwoFactorSubject.Register, "register"),
        new KeyValuePair<TwoFactorSubject, string>(TwoFactorSubject.PasswordReset, "password_reset"),
        new KeyValuePair<TwoFactorSubject, string>(TwoFactorSubject.ToggleTwoFactor, "toggle_twofactor"),
    };
}