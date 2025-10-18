namespace Paribu.Api.Models;

internal class ParibuRestAppResponse<T>
{
    // [JsonProperty("meta")]
    // public ParibuRestAppMeta? Meta { get; set; }

    [JsonProperty("message")]
    public ParibuRestAppMessage? Message { get; set; }

    [JsonProperty("payload")]
    public T? Payload { get; set; }

    public bool Success { get => Payload != null; }
}

internal class ParibuRestAppMeta
{
}

internal class ParibuRestAppMessage
{
    [JsonProperty("display")]
    public ParibuRestAppDisplay Display { get; set; } = default!;

    [JsonProperty("severity")]
    public string Severity { get; set; } = "";

    [JsonProperty("title")]
    public ParibuLanguageResource? Title { get; set; }

    [JsonProperty("description")]
    public ParibuLanguageResource? Description { get; set; }
}

internal class ParibuRestAppDisplay
{
    [JsonProperty("component")]
    public string Component { get; set; } = "";

    [JsonProperty("content")]
    public string Content { get; set; } = "";
}

internal class ParibuLanguageResource
{
    [JsonProperty("langkey")]
    public string LanguageKey { get; set; } = "";
}