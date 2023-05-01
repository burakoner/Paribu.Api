namespace Paribu.Api.Models.RestApi;

public class ParibuNetwork
{
    public string Name { get; set; }
    public string Symbol { get; set; }

    [JsonProperty("network_type")]
    public string NetworkType { get; set; }

    [JsonProperty("explorer"), JsonConverter(typeof(SafeCollectionConverter))]
    public IEnumerable<ParibuNetworkExplorer> Explorers { get; set; }

    [JsonProperty("validations"), JsonConverter(typeof(SafeCollectionConverter))]
    public IEnumerable<ParibuNetworkValidations> Validations { get; set; }
}

public class ParibuNetworkExplorer
{
    public string Address { get; set; }
    public string Transaction { get; set; }
}

public class ParibuNetworkValidations
{
    [JsonProperty("address_regex")]
    public string AddressRegex { get; set; }
}
