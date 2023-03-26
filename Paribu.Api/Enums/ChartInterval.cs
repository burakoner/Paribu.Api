namespace Paribu.Api.Enums;

public enum ChartInterval
{
    /// <summary>
    /// Toplam 6 saatlik süredeki grafik verilerini gösterir
    /// Candlestick Period: 5 Dakika
    /// </summary>
    SixHours,

    /// <summary>
    /// Toplam 1 günlük süredeki grafik verilerini gösterir
    /// Candlestick Period: 20 Dakika
    /// </summary>
    OneDay,

    /// <summary>
    /// Toplam 1 haftalık süredeki grafik verilerini gösterir
    /// Candlestick Period: 2 Saat
    /// </summary>
    OneWeek,

    /// <summary>
    /// Toplam 1 aylık süredeki grafik verilerini gösterir
    /// Candlestick Period: 8 Saat
    /// </summary>
    OneMonth,

    /// <summary>
    /// Toplam 3 aylık süredeki grafik verilerini gösterir
    /// Candlestick Period: 1 Gün
    /// </summary>
    ThreeMonths,

    /// <summary>
    /// Toplam 6 aylık süredeki grafik verilerini gösterir
    /// Candlestick Period: 2 Gün
    /// </summary>
    SixMonths,
}