using System;
using System.Runtime.InteropServices;

namespace ProLab.Services.Time
{
    public static class TimeService
    {
        private static TimeZoneInfo? _stockholmTz;

        public static TimeZoneInfo StockholmTimeZone
        {
            get
            {
                if (_stockholmTz != null)
                    return _stockholmTz;

                _stockholmTz = TimeZoneInfo.FindSystemTimeZoneById(
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                        ? "W. Europe Standard Time"
                        : "Europe/Stockholm"
                );

                return _stockholmTz;
            }
        }

        // ==========================
        // LOCAL → UTC (för DB)
        // ==========================
        public static DateTimeOffset LocalToUtc(DateTime localDateTime)
        {
            var unspecified = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);

            var utc = TimeZoneInfo.ConvertTimeToUtc(
                unspecified,
                StockholmTimeZone
            );

            return new DateTimeOffset(utc, TimeSpan.Zero);
        }

        // ==========================
        // UTC → LOCAL (för UI)
        // ==========================
        public static DateTimeOffset UtcToLocal(DateTimeOffset utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                utc.UtcDateTime,
                StockholmTimeZone
            );
        }
    }
}