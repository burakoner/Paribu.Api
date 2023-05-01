namespace Paribu.Api.Models;

internal class ParibuRestApiResponse<T>
{
    //[JsonProperty("meta")]
    //public ParibuRestApiMesta Meta { get; set; }

    [JsonProperty("message")]
    public ParibuRestApiMessage Message { get; set; }
    //[JsonProperty("message")]
    //public string Message { get; set; }

    [JsonProperty("payload")]
    public T Payload { get; set; }
    public bool Success { get => Payload != null; }
}