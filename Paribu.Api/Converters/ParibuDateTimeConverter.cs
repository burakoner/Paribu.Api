using Newtonsoft.Json.Converters;

namespace Paribu.Api.Converters;

internal class ParibuDateTimeConverter : IsoDateTimeConverter
{
    public ParibuDateTimeConverter()
    {
        DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
    }
}
