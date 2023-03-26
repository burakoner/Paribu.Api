namespace Paribu.Api.Models.RestApi;

public class ParibuTransaction
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(TransactionDirectionConverter))]
    public TransactionDirection Direction { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("tx")]
    public string TransactionId { get; set; }

    [JsonProperty("txhref")]
    public string TransactionLink { get; set; }

    [JsonProperty("addresshref")]
    public string AddressLink { get; set; }

    [JsonProperty("cancellable")]
    public bool Cancellable { get; set; }

    [JsonProperty("confirmations")]
    public int Confirmations { get; set; }

    [JsonProperty("status"), JsonConverter(typeof(TransactionStatusConverter))]
    public TransactionStatus Status { get; set; }

    [JsonProperty("counterparty")]
    public string CounterParty { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("timestamp"), JsonConverter(typeof(TimestampSecondsConverter))]
    public DateTime Timestamp { get; set; }
}
