namespace Paribu.Api.Models.StreamApi;

public class ParibuStreamPatch<T>
{
    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("patch")]
    public T Patch { get; set; }
}

public class ParibuStreamMerge<T>
{
    [JsonProperty("unset")]
    public IEnumerable<string> Unset { get; set; }

    [JsonProperty("merge")]
    public T Merge { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<ParibuStreamTickers>))]
public class ParibuStreamTickers
{
    [TypedData]
    public Dictionary<string, ParibuStreamTicker> Data { get; set; }
}

public class StreamOrderBook
{
    [JsonProperty("buy")]
    public StreamOrderBookEntries Bids { get; set; }

    [JsonProperty("sell")]
    public StreamOrderBookEntries Asks { get; set; }
}

[JsonConverter(typeof(TypedDataConverter<StreamOrderBookEntries>))]
public class StreamOrderBookEntries
{
    [TypedData]
    public Dictionary<decimal, decimal> Data { get; set; }
}