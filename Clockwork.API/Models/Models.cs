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


        public int[] ConvertToDateArray()
        {
            int[] dateArray = {
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second
            };
            return dateArray;
        }

        public static LocalDateTime LocalDateTime()
        {

            CurrentTimeQuery ctq = new CurrentTimeQuery();
            int[] isoDate = ctq.ConvertToDateArray();
            LocalDateTime timeZoneSelectionServerTime = 
                new LocalDateTime(isoDate[0], isoDate[1], isoDate[2], isoDate[3], isoDate[4], isoDate[5]);
            return timeZoneSelectionServerTime;

        }

        public static DateTime ConvertToDifferentTimeZoneFromUtc(string timeZone)
        {
            var newTimeZoneTime = DateTimeZoneProviders.Tzdb[timeZone];
            return Instant.FromDateTimeUtc(DateTime.UtcNow)
                          .InZone(newTimeZoneTime)
                          .ToDateTimeUnspecified();
        }

    }


}
