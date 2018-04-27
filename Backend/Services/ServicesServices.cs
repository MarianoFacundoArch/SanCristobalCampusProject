using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using DataSqlServer;

namespace Services
{
    public class ServicesServices
    {
        public bool RequestService(int userId, ServiceRequestDTO serviceRequest)
        {
            using (var entities = new UCAInvestDBEntities())
            {
                var userDbResponse = entities.users.SingleOrDefault(t => t.user_id == userId);
                if (userDbResponse != null)
                {
                    if (userDbResponse.status == "service_requested")
                    {
                        return false;
                    }
                    long timeStamp = (long) TimeFunctions.DateTimeToUnixTimestamp(DateTime.Now);
                    var location = LocationServices.GenerateDBGeographyFromCoordinates(serviceRequest.coordinates);
                    UserService serviceToAdd = new UserService()
                    {
                        client_user_id = userId,
                        request_timestamp = timeStamp,
                        location = location,
                        description = serviceRequest.description,
                        taken_timestamp = 0,
                        completed_timestamp = 0
                    };
                    entities.user_services.Add(serviceToAdd);
                    userDbResponse.status = "service_requested";
                    entities.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void FinishService(int userId, int serviceId)
        {
            using (var entities = new UCAInvestDBEntities())
            {

                var serviceDbResponse =
                    entities.user_services.SingleOrDefault(t =>
                        (t.crane_user_id == userId || t.client_user_id == userId) && t.service_id == serviceId);

                serviceDbResponse.ServiceUser.status = "freeuser";
                serviceDbResponse.CraneUser.status = "available_crane";
                serviceDbResponse.completed_timestamp = (long) TimeFunctions.DateTimeToUnixTimestamp(DateTime.Now);
                entities.SaveChanges();

            }
        }


        public CurrentServicePositionsDTO ServicePosition (int userId)
        {
            CurrentServicePositionsDTO response = null;
            using (var entities = new UCAInvestDBEntities())
            {
                var serviceDbResponse =
                    entities.user_services.SingleOrDefault(t =>
                        (t.crane_user_id == userId || t.client_user_id == userId ) && t.completed_timestamp== 0);
                if (serviceDbResponse != null)
                {
                    response = new CurrentServicePositionsDTO();
                    var cranePosition = new CoordinateDTO(
                        (double) serviceDbResponse.CraneUser.user_locations.location.Latitude,
                        (double) serviceDbResponse.CraneUser.user_locations.location.Longitude);
                    if (userId == serviceDbResponse.crane_user_id)
                    {
                        response.OtherUser = new UsersServices().PublicUserById((int) serviceDbResponse.client_user_id);
                        response.OtherUserPosition = new CoordinateDTO((double)serviceDbResponse.ServiceUser.user_locations.location.Latitude,
                            (double)serviceDbResponse.ServiceUser.user_locations.location.Longitude);
                    }
                    else
                    {
                        response.OtherUser = new UsersServices().PublicUserById((int)serviceDbResponse.crane_user_id);
                        response.OtherUserPosition = cranePosition;
                    }
                    
                       
                    response.RequestedPosition = new CoordinateDTO((double)serviceDbResponse.location.Latitude,
                        (double)serviceDbResponse.location.Longitude);

                    response.ServiceId = serviceDbResponse.service_id;
                    response.TimeRemaining = new GoogleTimeService().timeLeft(cranePosition, response.RequestedPosition);
                }
            }
            return response;
        }
        public bool TakeService(int craneUserId, int serviceId )
        {
            using (var entities = new UCAInvestDBEntities())
            {

                var craneUser = entities.users.SingleOrDefault(t => t.user_id == craneUserId);
                if (craneUser==null||craneUser.status=="unavailable_crane")
                {
                    return false;
                }
                craneUser.status = "unavailable_crane";
                entities.SaveChanges();


                var serviceResponse = entities.user_services.SingleOrDefault(t => t.service_id == serviceId);
                if (serviceResponse != null && serviceResponse.taken_timestamp == 0)
                {
                    serviceResponse.taken_timestamp = (long?) TimeFunctions.DateTimeToUnixTimestamp(DateTime.Now);
                    serviceResponse.crane_user_id = craneUserId;
                    var serviceUser = serviceResponse.ServiceUser;
                    if (serviceUser != null)
                    {
                        serviceUser.status = "waiting_crane_arrival";
                    }
                    entities.SaveChanges();

                    return true;
                }

                
                return false;
            }
        }
    }
}
