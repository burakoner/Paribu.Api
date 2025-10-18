namespace Paribu.Api.Models.RestApi;

public record ParibuAppInformation
{
    [JsonProperty("currencies")]
    internal Dictionary<string, ParibuAppAsset> Currencies { get; set; } = [];
    public List<ParibuAppAsset> Assets => [.. Currencies.Select(x => x.Value.SetSymbol(x.Key))];


    [JsonProperty("markets")]
    internal Dictionary<string, ParibuAppMarket> Pairs { get; set; } = [];
    public List<ParibuAppMarket> Markets => [.. Pairs.Select(x => x.Value.SetSymbol(x.Key))];
}