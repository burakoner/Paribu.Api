namespace Paribu.Api.Converters;

public class TransactionStatusConverter : BaseConverter<TransactionStatus>
{
    public TransactionStatusConverter() : this(true) { }
    public TransactionStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<TransactionStatus, string>> Mapping => new List<KeyValuePair<TransactionStatus, string>>
    {
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.WaitingSmsApproval, "pending-2fa-approval"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.WaitingEmailApproval, "pending-email-approval"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.PendingConfirmation, "pending-confirmation"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.Verified, "verified"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.Failed, "failed"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.Queued, "queued"),
        new KeyValuePair<TransactionStatus, string>(TransactionStatus.Started, "started"),
    };
}