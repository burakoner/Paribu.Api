namespace Paribu.Api.Enums;

public enum ParibuKlineInterval
{
    [Label("15")]
    FifteenMinutes,

    [Label("60")]
    OneHour,

    [Label("240")]
    FourHours,

    [Label("1D")]
    OneDay,
}