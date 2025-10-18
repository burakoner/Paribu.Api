namespace Paribu.Api.Models.RestApi;

public record ParibuPaging
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}