namespace Paribu.Api.Models.RestApi;

public record ParibuTransferHistory
{
    [JsonProperty("paging")]
    public ParibuPaging Paging { get; set; } = default!;

    [JsonProperty("transfers")]
    public List<ParibuTransfer> Transfers { get; set; } = [];
}

public record ParibuTransfer
{
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("createdAt"), JsonConverter(typeof(ParibuDateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("crossAddress")]
    public string CrossAddress { get; set; } = "";

    [JsonProperty("currency")]
    public string Asset { get; set; } = "";

    [JsonProperty("deletedAt"), JsonConverter(typeof(ParibuDateTimeConverter))]
    public DateTime? DeletedAt { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; } = "";

    [JsonProperty("network")]
    public string Network { get; set; } = "";

    [JsonProperty("status")]
    public string Status { get; set; } = "";

    [JsonProperty("transferId")]
    public string TransferId { get; set; } = "";

    [JsonProperty("tx")]
    public string TransactionId { get; set; } = "";

    [JsonProperty("userId")]
    public string UserId { get; set; } = "";

    [JsonProperty("verifiedAt"), JsonConverter(typeof(ParibuDateTimeConverter))]
    public DateTime? VerifiedAt { get; set; }
}