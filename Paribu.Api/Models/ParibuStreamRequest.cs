namespace Paribu.Api.Models;

public class ParibuStreamRequest<T>
{
    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}

public class ParibuSocketSubscribeRequest
{
    [JsonProperty("auth")]
    public string Auth { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }
}
