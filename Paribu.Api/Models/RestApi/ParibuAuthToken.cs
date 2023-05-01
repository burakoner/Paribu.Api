namespace Paribu.Api.Models.RestApi;

public class ParibuAuthToken
{
    [JsonProperty("auth_token")]
    public string AuthenticationToken { get; set; }
}