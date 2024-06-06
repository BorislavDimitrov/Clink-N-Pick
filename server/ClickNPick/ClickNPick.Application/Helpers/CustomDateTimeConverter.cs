using Newtonsoft.Json.Converters;

namespace ClickNPick.Application.Helpers;

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
        : this("yyyy-MM-dd HH:mm:ss")
    {
    }

    public CustomDateTimeConverter(string dateTimeFormat)
    {
        DateTimeFormat = dateTimeFormat;
    }
}
