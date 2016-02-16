using System;

namespace Bicimad.Helpers
{
    public static class DateTimeHelper
    {
        private const string SPAIN_ZONE_ID = "Romance Standard Time";

        public static DateTime SpanishNow
        {
            get
            {
                var cstZone = TimeZoneInfo.FindSystemTimeZoneById(SPAIN_ZONE_ID);
                var cstTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);
                return cstTime;
            }
        }

        public static string ToDateFormat(DateTime date)
        {
            return date.ToShortDateString();
        }

        public static string ToDateTimeFormat(DateTime date)
        {
            return ToDateFormat(date) + " " + date.ToShortTimeString();
        }
    }
}