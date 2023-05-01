namespace Paribu.Api.Models.RestApi;

public class ParibuKline
{
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public long Timestamp { get; set; }
    public DateTime Time { get => Timestamp.ConvertFromSeconds(); }

    internal static IEnumerable<ParibuKline> ImportChartHistory(ParibuChartHistory data)
    {
        var list = new List<ParibuKline>();
        var min = data.Open.Count;
        min = Math.Min(min, data.High.Count);
        min = Math.Min(min, data.Low.Count);
        min = Math.Min(min, data.Close.Count);
        min = Math.Min(min, data.Volume.Count);
        min = Math.Min(min, data.Timestamp.Count);
        for (var i = 0; i < min; i++)
        {
            list.Add(new ParibuKline
            {
                Open = data.Open[i],
                High = data.High[i],
                Low = data.Low[i],
                Close = data.Close[i],
                Volume = data.Volume[i],
                Timestamp = data.Timestamp[i],
            });
        }

        return list;
    }
}

internal class ParibuChartHistory
{
    [JsonProperty("ttl")]
    public int TTL { get; set; }

    [JsonProperty("s")]
    public string Status { get; set; }
    
    [JsonProperty("nextTime")]
    public long? NextTime { get; set; }

    [JsonProperty("o")]
    public List<decimal> Open { get; set; }

    [JsonProperty("h")]
    public List<decimal> High { get; set; }

    [JsonProperty("l")]
    public List<decimal> Low { get; set; }

    [JsonProperty("c")]
    public List<decimal> Close { get; set; }

    [JsonProperty("v")]
    public List<decimal> Volume { get; set; }

    [JsonProperty("t")]
    public List<long> Timestamp { get; set; }
}