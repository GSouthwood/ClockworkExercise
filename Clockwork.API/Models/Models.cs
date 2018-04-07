using System;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Clockwork.API.Models
{
    public class ClockworkContext : DbContext
    {
        public DbSet<CurrentTimeQuery> CurrentTimeQueries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=clockwork.db");
        }
    }

    public class CurrentTimeQuery
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        public string TimeZoneSelection { get; set; }
        //would be changing it to a string in the view anyway
        //so it is made a string here instead
        public string TimeZoneSelectionTime { get; set; }

        //Convert to the selected time zone's local time from the server time in UTC
        public static DateTime ConvertToDifferentTimeZoneFromUtc(string timeZone)
        {
            var newTimeZoneTime = DateTimeZoneProviders.Tzdb[timeZone];
            return Instant.FromDateTimeUtc(DateTime.UtcNow)
                          .InZone(newTimeZoneTime)
                          .ToDateTimeUnspecified();
        }

        //get the full name of the location associated with the time zone id
        //to be used in the GET
        public static string GetZoneId(string longZoneId)
        {
            return longZoneId.Substring(longZoneId.IndexOf("-") + 2);
        }

    }


}
