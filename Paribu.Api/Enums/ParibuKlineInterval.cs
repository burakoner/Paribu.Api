namespace Paribu.Api.Enums;

public enum ParibuKlineInterval
{
    [Map("15")]
    FifteenMinutes = 900,

    [Map("60")]
    OneHour = 3600,

    [Map("240")]
    FourHours = 14400,

    [Map("1D")]
    OneDay = 86400,
}