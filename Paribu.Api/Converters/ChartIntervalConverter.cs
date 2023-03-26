namespace Paribu.Api.Converters;

public class ChartIntervalConverter : BaseConverter<ChartInterval>
{
    public ChartIntervalConverter() : this(true) { }
    public ChartIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<ChartInterval, string>> Mapping => new List<KeyValuePair<ChartInterval, string>>
    {
        new KeyValuePair<ChartInterval, string>(ChartInterval.SixHours, "6h"),
        new KeyValuePair<ChartInterval, string>(ChartInterval.OneDay, "1d"),
        new KeyValuePair<ChartInterval, string>(ChartInterval.OneWeek, "7d"),
        new KeyValuePair<ChartInterval, string>(ChartInterval.OneMonth, "1m"),
        new KeyValuePair<ChartInterval, string>(ChartInterval.ThreeMonths, "3m"),
        new KeyValuePair<ChartInterval, string>(ChartInterval.SixMonths, "6m"),
    };
}