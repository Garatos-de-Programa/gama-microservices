namespace Gama.Shared.Extensions
{
    public static class DatetimeExtensions
    {
        public const string BrazilianTimeZoneId = "E. South America Standard Time";

        public static DateTime? ToTimeZone(this DateTime? dateTime, string timeZoneId) 
        {
            if (dateTime is null)
            {
                return null;
            }
            
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, timezone);
        }

        public static DateTime? ToUtc(this DateTime? dateTime, string fromTimeZoneId)
        {
            if (dateTime is null)
            {
                return null;
            }

            var timezone = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime.Value, timezone);
        }

        public static DateTime? ToUtc(this DateTime dateTime, string fromTimeZoneId)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timezone);
        }
    }
}
