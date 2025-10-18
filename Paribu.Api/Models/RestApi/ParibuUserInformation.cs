namespace Paribu.Api.Models.RestApi;

public record ParibuUserInformation
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("first_name")]
    public string FirstName { get; set; } = "";

    [JsonProperty("last_name")]
    public string LastName { get; set; } = "";

    [JsonProperty("country_code")]
    public string CountryCode { get; set; } = "";
}