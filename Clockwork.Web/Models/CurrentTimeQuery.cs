using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NodaTime;

namespace Clockwork.Web.Models
{
    public class CurrentTimeQuery
    {
        public int CurrentTimeQueryId { get; set; }
        public DateTime Time { get; set; }
        public string ClientIp { get; set; }
        public DateTime UTCTime { get; set; }
        //create a property to hold the time zone selection for storing in the database
        public string TimeZoneSelection { get; set; }
        //A list created to hold the different options for time zones
        public List<SelectListItem> TimeZoneList
        {
            get
            {
                return TimeZoneList;
            }
            set
            {
                TimeZoneList = GetTimeZones();
            }
        }
        //Time after conversion from time zone selection
        public string TimeZoneSelectionTime { get; set; }

        public static List<SelectListItem> GetTimeZones()
        {
            List<SelectListItem> timeZonesList = new List<SelectListItem>();
            SelectListItem s = new SelectListItem();
            s.Text = "--";
            timeZonesList.Add(s);
            foreach (var id in DateTimeZoneProviders.Tzdb.Ids)
            {
                SelectListItem l = new SelectListItem();
                l.Text = DateTimeZoneProviders.Tzdb[id].ToString();
                timeZonesList.Add(l);

            }
            return timeZonesList;
        }



    }
}