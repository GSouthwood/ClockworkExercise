using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using Microsoft.AspNetCore.Cors;
using NodaTime;

namespace Clockwork.API.Controllers
{
    
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime

        [HttpGet]
        public IActionResult Get(string timeZone)
        {
            //gets the current utc time
            DateTime utcTime = DateTime.UtcNow;
            
            //get current server time 
            DateTime serverTime = DateTime.Now;
            
            //get the ip address of the user
            string ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            //set default timeZoneSelection to an empty string if they didn't select anything
            //aka selected '--'
            string timeZoneSelection = "";

            //timeZoneSelectionTime for displaying and adding to database
            string timeZoneSelectionTime = "";


            //if a timeZone has been selected, then the server time is used to calculate the 
            //zoned date time of the selection
            if (timeZone != null && timeZone != "--")
            {
                //get the requested timeZone
                //DateTimeZone tzTime = DateTimeZoneProviders.Tzdb[timeZone];
                timeZoneSelection = timeZone;
                timeZoneSelectionTime = CurrentTimeQuery
                    .ConvertToDifferentTimeZoneFromUtc(CurrentTimeQuery.GetZoneId(timeZone)).ToShortTimeString();
                
            }


            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = serverTime,
                TimeZoneSelectionTime = timeZoneSelectionTime,
                TimeZoneSelection = timeZoneSelection 
            };

            using (var db = new ClockworkContext())
            {
                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery.UTCTime);
                }
            }

            return Ok(returnVal);
        }
    }
}
