namespace Paribu.Api.Models;


internal class ParibuRestApiMessage
{
    [JsonProperty("display")]
    public ParibuRestApiDisplay Display { get; set; }

    [JsonProperty("severity")]
    public string Severity { get; set; }

    [JsonProperty("title")]
    public ParibuLanguageResource Title { get; set; }

    [JsonProperty("description")]
    public ParibuLanguageResource Description { get; set; }
}

internal class ParibuRestApiDisplay
{
    [JsonProperty("component")]
    public string Component { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }
}

public class ParibuLanguageResource
{
    [JsonProperty("langkey")]
    public string LanguageKey { get; set; }
}