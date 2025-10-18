namespace Paribu.Api.Models;

internal class ParibuStreamResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("method"), JsonConverter(typeof(MapConverter))]
    public ParibuStreamRequestMethod Method { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; } = "";

    [JsonProperty("status")]
    public string Status { get; set; } = "";

    [JsonProperty("channel")]
    public string Channel { get; set; } = "";
}

/*
public class PusherStreamResponse
{
    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("data")]
    public string Data { get; set; }

    [JsonProperty("channel")]
    public string Channel { get; set; }
}
*/

internal class ParibuStreamContainer<T>
{
    [JsonProperty("e")]
    public string Event { get; set; } = "";

    [JsonProperty("E"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("s")]
    public string Symbol { get; set; } = "";

    [JsonProperty("r")]
    public T Payload { get; set; } = default!;
}