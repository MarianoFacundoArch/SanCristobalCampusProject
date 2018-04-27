using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using Newtonsoft.Json;

namespace Services
{
    public class GoogleTimeService
    {
        public static string googleApiKey = "AIzaSyDKuHzxrnPUT1kHdFbdmie_-XbiOE7McNI";
        public string timeLeft(CoordinateDTO origin, CoordinateDTO destination)
        {
            HttpClient clientRequest = new HttpClient();
            //clientRequest.Timeout = TimeSpan.FromSeconds(GlobalData.TimeoutWebRequests);

            var requestWebsite = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" +
                                 origin.latitude.ToString().Replace(",",".") + "," + origin.longitude.ToString().Replace(",", ".") + "&destinations=" + destination.latitude.ToString().Replace(",", ".") +
                                 "," + destination.longitude.ToString().Replace(",", ".") + "&key=" + googleApiKey;
            var contents = clientRequest.GetStringAsync(requestWebsite).Result;

            var result = JsonConvert.DeserializeObject<GoogleMapQuery>(contents);
            return result.Rows.FirstOrDefault().Elements.FirstOrDefault().Duration.Text;
        }
    }
}
