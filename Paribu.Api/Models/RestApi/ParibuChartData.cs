namespace Paribu.Api.Models.RestApi;

public class ParibuChartData
{
    public string Symbol { get; set; }

    public ChartInterval Interval { get; set; }

    public List<ParibuCandle> Candles { get; set; }

    public ParibuChartData()
    {
        Candles = new List<ParibuCandle>();
    }
}

public class ParibuCandle
{
    public int OpenTime { get; set; }

    public DateTime OpenDateTime { get; set; }

    public decimal ClosePrice { get; set; }

    public decimal Volume { get; set; }
}

internal class ChartData
{
    [JsonProperty("market")]
    public string Symbol { get; set; }

    [JsonProperty("interval"), JsonConverter(typeof(ChartIntervalConverter))]
    public ChartInterval Interval { get; set; }

    [JsonProperty("t")]
    public IEnumerable<int> OpenTimeData { get; set; }

    [JsonProperty("c")]
    public IEnumerable<decimal> ClosePriceData { get; set; }

    [JsonProperty("v")]
    public IEnumerable<decimal> VolumeData { get; set; }
}