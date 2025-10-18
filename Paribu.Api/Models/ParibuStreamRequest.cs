namespace Paribu.Api.Models;

/*
public class PusherStreamRequest<T>
{
    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}

public class PusherSocketSubscribeRequest
{
    [JsonProperty("auth")]
    public string Auth { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }
}
*/

internal class ParibuStreamRequest
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("method"), JsonConverter(typeof(MapConverter))]
    public ParibuStreamRequestMethod Method { get; set; }

    [JsonProperty("channels")]
    public List<string> Channels { get; set; } = [];
}

public enum ParibuStreamRequestMethod
{
    [Map("subscribe")]
    Subscribe,

    [Map("unsubscribe")]
    Unsubscribe
}
