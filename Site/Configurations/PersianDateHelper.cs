using System.Globalization;

namespace Site.Configurations;

public static class PersianDateHelper
{
    public static string ToPersianDate(this DateTime dateTime)
    {
        PersianCalendar pc = new();

        return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)}";
    }
}