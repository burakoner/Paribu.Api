namespace Paribu.Api.Models.RestApi;

public record ParibuDepositAddress
{
    [JsonProperty("id")]
    public string Id { get; set; } = "";

    [JsonProperty("user_id")]
    public string UserId { get; set; } = "";

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("network")]
    public string Network { get; set; } = "";

    [JsonProperty("address")]
    public string Address { get; set; } = "";

    [JsonProperty("tag")]
    public string? Tag { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonProperty("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}