using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data.Common;
using Clockwork.Web.Models;

namespace Clockwork.Web.DAL
{
    public class CurrentTimeQuerySqlDAL : ICurrentTimeQueryDAL
    {
        private readonly string connectionString;
        private const string SQLite_GetCurrentTimeQueries = "SELECT * FROM CurrentTimeQueries";

        public CurrentTimeQuerySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CurrentTimeQuery> GetCurrentTimeQuery()
        {
            List<CurrentTimeQuery> currentTimeQueries = new List<CurrentTimeQuery>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(SQLite_GetCurrentTimeQueries, conn);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CurrentTimeQuery c = new CurrentTimeQuery();

                        c.ClientIp = Convert.ToString(reader["ClientIp"]);
                        c.CurrentTimeQueryId = Convert.ToInt32(reader["CurrentTimeQueryId"]);
                        c.Time = Convert.ToDateTime(reader["Time"]);
                        c.UTCTime = Convert.ToDateTime(reader["UTCTime"]);
                        c.TimeZoneSelection = Convert.ToString(reader["TimeZoneSelection"]);
                        c.TimeZoneSelectionTime = Convert.ToString(reader["TimeZoneSelectionTime"]);

                        currentTimeQueries.Add(c);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return currentTimeQueries;
        }
    }
}