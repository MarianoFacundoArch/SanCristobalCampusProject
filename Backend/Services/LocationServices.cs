using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using DataSqlServer;

namespace Services
{
    public class LocationServices
    {
        public void UpdatePosition(int userId, CoordinateDTO coordinates)
        {
            using (var entities = new UCAInvestDBEntities())
            {
                var dbResponse = entities.user_locations.SingleOrDefault(t => t.user_id == userId);
                var currentLocation = GenerateDBGeographyFromCoordinates(coordinates);
               var currentTimestamp = (long?)TimeFunctions.DateTimeToUnixTimestamp(DateTime.Now);
                if (dbResponse != null)
                {
                    dbResponse.location = currentLocation;
                    dbResponse.timestamp = currentTimestamp;
                }
                else
                {
                    entities.user_locations.Add(new UserLocation()
                    {
                        location = currentLocation,
                        timestamp = currentTimestamp,
                        user_id = userId
                    });
                }
                entities.SaveChanges();
            }
        }

        public List<MapPinDTO> GetNearCranes(CoordinateDTO coordinates)
        {
            var response = new List<MapPinDTO>();
            var currentLocation = GenerateDBGeographyFromCoordinates(coordinates);
            using (var entities = new UCAInvestDBEntities())
            {
                var Cranes = (from u in entities.user_locations
                    where u.users.is_provider == 1 &&
                          u.location.Distance(currentLocation) <= CommonConfig.IncidenceRadiusInM
                    orderby u.location.Distance(currentLocation)
                    select u);
                foreach (var crane in Cranes)
                {
                    response.Add(new MapPinDTO(crane.users.username,
                        new CoordinateDTO((double) crane.location.Latitude, (double) crane.location.Longitude),
                        crane.users.plate, "", (long) crane.timestamp, (int) crane.users.is_provider,crane.user_id));
                }

            }
            return response;
        }


        public List<ServicePinDTO> GetNearUntakenServices(CoordinateDTO coordinates)
            {
                var response = new List<ServicePinDTO>();
                var currentLocation = GenerateDBGeographyFromCoordinates(coordinates);
                using (var entities = new UCAInvestDBEntities())
                {
                var Services = (from u in entities.user_services
                    where u.taken_timestamp==0 && u.completed_timestamp==0 && u.location.Distance(currentLocation) <= CommonConfig.IncidenceRadiusInM
                    orderby u.location.Distance(currentLocation)
                    select u);

                    foreach (var service in Services)
                    {
                        response.Add(new ServicePinDTO(service.ServiceUser.username, new CoordinateDTO((double)service.location.Latitude, (double)service.location.Longitude),service.ServiceUser.plate, service.description , (long) service.request_timestamp,2,service.ServiceUser.user_id,service.service_id));
                    }

                }
                return response;
            }

        public static DbGeography GenerateDBGeographyFromCoordinates(CoordinateDTO coordinates)
        {
            string conversion = "POINT(" + coordinates.longitude + " " + coordinates.latitude + ")";
            conversion = conversion.Replace(",", ".");
            var myLocation = DbGeography.FromText(conversion);
            return myLocation;
        }
    }
}
