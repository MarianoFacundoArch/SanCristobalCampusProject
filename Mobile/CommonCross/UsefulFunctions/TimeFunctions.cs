using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCross.UsefulFunctions
{
    public class TimeFunctions
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return Math.Truncate((TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc) -
                                  new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds);
        }



        public static async Task Foo(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        public static DateTime TruncateToHoursDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
        }

        public static long TruncateToHoursUnixTime(long timeToTruncate)
        {
            DateTime requestedTime = UnixTimeStampToDateTime(timeToTruncate);

            DateTime resultTime = TruncateToHoursDateTime(requestedTime);

            return (long)DateTimeToUnixTimestamp(resultTime);
        }
    }
}
