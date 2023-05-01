namespace Paribu.Api.Models.RestApi;

public class ParibuExchangeInformation
{
    [JsonProperty("app_settings")]
    public ParibuAppSettings AppSettings { get; set; }

    [JsonProperty("currencies")]
    public Dictionary<string, ParibuAsset> Assets { get; set; }

    [JsonProperty("markets")]
    public Dictionary<string, ParibuMarket> Markets { get; set; }

    [JsonProperty("networks")]
    public Dictionary<string, ParibuNetwork> Networks { get; set; }

    [JsonProperty("fee_matrix")]
    public Dictionary<string, Dictionary<string, IEnumerable<Dictionary<string, decimal>>>> FeeMatrix { get; set; }
}