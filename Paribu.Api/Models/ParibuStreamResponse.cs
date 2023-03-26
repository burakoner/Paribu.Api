namespace Paribu.Api.Models;

public class ParibuStreamResponse
{
    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("data")]
    public string Data { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }
}
