using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NodaTime;
using TimeZoneNames;

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

        //create the dropdown list for selecting time zone
        public static List<SelectListItem> GetTimeZones()
        {
            List<SelectListItem> timeZonesList = new List<SelectListItem>();
            SelectListItem s = new SelectListItem();
            s.Text = "--";
            timeZonesList.Add(s);
            bool isPresent = false;
            foreach (var id in DateTimeZoneProviders.Tzdb.Ids)
            {
                SelectListItem l = new SelectListItem();
                if (timeZonesList.Count == 1)
                {
                    l.Text = DateTimeZoneProviders.Tzdb[id].ToString()
                        + " - " + TZNames.GetNamesForTimeZone(id, "en-US").Generic;
                    timeZonesList.Add(l);
                }          
                else if (timeZonesList.Count >= 2)
                {
                    for (int i = 1; i < timeZonesList.Count; i++)
                    {
                        if (timeZonesList[i].Text.Contains("-") && timeZonesList[i].Text.
                            Substring(0, timeZonesList[i].Text.IndexOf("-"))
                            .Equals(TZNames.GetNamesForTimeZone(id, "en-US").Generic))
                        {
                            isPresent = true;
                        }
                    }
                    if (!isPresent)
                    {
                        l.Text = DateTimeZoneProviders.Tzdb[id].ToString()
                        + " - " + TZNames.GetNamesForTimeZone(id, "en-US").Generic;
                        timeZonesList.Add(l);
                    }
                    isPresent = false;
                }
                
                

            }
            return timeZonesList;
        }

        //get the name of the location associated with the time zone id
        //without the country
        public static string GetShortLocationName(string longZoneId)
        {
            string name = longZoneId
                .Substring(longZoneId.IndexOf("-") + 2);

            return name.Substring(name.IndexOf("/") + 1).Replace("_", " ");
        }

        //get the generic name of the time zone that is selected
        public static string GetShortTimeZoneName(string longZoneId)
        {
            if (longZoneId == "")
            {
                return "";
            }
            return longZoneId.Substring(0, longZoneId.IndexOf("-"));
            
                
            
        }
        


    }
}