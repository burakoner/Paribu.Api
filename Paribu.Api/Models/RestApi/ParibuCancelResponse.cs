namespace Paribu.Api.Models.RestApi;

public class ParibuCancelResponse
{
    [JsonProperty("deleted")]
    public bool Canceled { get; set; }
}