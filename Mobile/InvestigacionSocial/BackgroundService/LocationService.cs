using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonCross.DTOS;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.Views;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Notifications;
using Xamarin.Forms;

namespace InvestigacionSocial.BackgroundService
{
    public class LocationService
    {
        public static bool IsRunning = false;
        public static string current;
        public static bool firstRun = true;



        public static void WriteServiceInfoToServiceFile(bool running, string token, string status)
        {
            ServiceData serviceData = new ServiceData();
            serviceData.token = token;
            serviceData.IsRunning = running;
            serviceData.status = status;

            FileSystem.Current.LocalStorage.CreateFileAsync("service.json", CreationCollisionOption.ReplaceExisting)
                .Result.WriteAllTextAsync(JsonConvert.SerializeObject(serviceData));
        }

        public static ServiceData ReadServiceInfoFromServiceFile()
        {
            var servicePropertiesExists =
                FileSystem.Current.LocalStorage.CheckExistsAsync("service.json").Result;
            if (servicePropertiesExists != ExistenceCheckResult.FileExists)
            {
                return null;
            }
            IFile fileRead = FileSystem.Current.LocalStorage.GetFileAsync("service.json").Result;
            var serviceProperties = fileRead.ReadAllTextAsync().Result;
            ServiceData ServiceData = JsonConvert.DeserializeObject<ServiceData>(serviceProperties);
            return ServiceData;
        }

        public static void StartServiceInCorrespondingPlatformIfNotRunning()
        {
            //TODO: Review this. Because it autorestarts service each time, and shouldn't.
            if (PositionServiceIsRunning() == true)
            {
                try
                {
                    MessagingCenter.Send(new StopLongRunningTaskMessage(), "StopLongRunningTaskMessage");
                }
                catch (Exception e)
                {

                }
            }

            WriteServiceInfoToServiceFile(true, GlobalData.LoginData.token, GlobalData.myUser.status);

            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, "StartLongRunningTaskMessage");
        }




        public static bool PositionServiceIsRunning()
        {
            if (LocationService.IsRunning)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task RunCounter(CancellationToken token)
        {
            IsRunning = true;
            await Task.Run(async () =>
            {
                try
                {

                    /*
                     * Check if service.json exists and should run the process
                     * */

                    ServiceData serviceData = ReadServiceInfoFromServiceFile();

                    if (serviceData == null)
                    {
                        return;
                    }

                    if (serviceData.IsRunning == false)
                        return;


                    bool finished = false;
                    string locationUpdateUrlAndRetrieveStatus = CommonCross.CommonConfig.BackendBase + "Locations/Update" +
                                      "?token=" + serviceData.token;

                    string nearUntakenServicesUrl = CommonCross.CommonConfig.BackendBase + "Locations/NearUntakenServicesCount" +
                                               "?token=" + serviceData.token;


                    HttpClient clientRequest = new HttpClient();

                    int temporizerForReNotifications = 0;
                    bool wasCraneSolicitudeTakeNotified = false;
                    while (!finished)
                    {

                        token.ThrowIfCancellationRequested();

                        await Task.Delay(GlobalData.BackgroundServiceInterval);



                        var myPosition = GeoLocatorService.GetCurrentPosition();
                        StringContent content;
                        string contents;
                        HttpResponseMessage result;
                        bool statusWasChanged = false;
                        try 
                        {

                            content = new StringContent(JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude, myPosition.Result.Longitude)), Encoding.UTF8, "application/json");
                            result = await clientRequest.PostAsync(locationUpdateUrlAndRetrieveStatus, content);
                            contents = await result.Content.ReadAsStringAsync();
                            var StatusChange = JsonConvert.DeserializeObject<StatusChangeDTO>(contents);
                            statusWasChanged = StatusChange.status != serviceData.status;
                            serviceData.status = StatusChange.status;
                            if (GlobalData.myUser != null)
                            {
                                GlobalData.myUser.status = StatusChange.status;
                            }
                                



                        }
                        catch (Exception e)
                        {

                        }

                        if (serviceData.status=="freeuser")
                        {
                            StopService();
                        }
                        if (serviceData.status == "unavailable_crane" && statusWasChanged)
                        {
                            RemoveNotification();

                        }

                        if (serviceData.status == "service_requested" && statusWasChanged)
                        {
                            NotifyWaitingForCraneAsignal();

                        }

                        if (serviceData.status == "waiting_crane_arrival" && statusWasChanged)
                        {
                            NotifyCraneTakenSolicitude();

                        }


                        if (serviceData.status == "available_crane")
                        {
                            /*
                            * Send location updates
                            * 
                            * */

                            /* Not needed now as is updated while checking or new services.
                             * try
                            {

                                content = new StringContent(JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude, myPosition.Result.Longitude)), Encoding.UTF8, "application/json");
                                result = await clientRequest.PostAsync(locationUpdateUrl, content);
                                contents = await result.Content.ReadAsStringAsync();


                            }
                            catch (Exception e)
                            {

                            }
                            */
                            /*
                             * Check if any untaken service near
                             * */

                            try
                            {
                                content = new StringContent(JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude, myPosition.Result.Longitude)), Encoding.UTF8, "application/json");
                                result = await clientRequest.PostAsync(nearUntakenServicesUrl, content);
                                contents = await result.Content.ReadAsStringAsync();

                                var untakenServicesCount =
                                    JsonConvert.DeserializeObject<NearUntakenServicesCountDTO>(contents);
                                if (untakenServicesCount.count > 0)
                                {
                                    if (temporizerForReNotifications == 0)
                                    {
                                        NotifyPendingServices();
                                        temporizerForReNotifications++;

                                    }
                                    else
                                    {
                                        if (temporizerForReNotifications==30)
                                        {
                                            NotifyPendingServices();
                                            temporizerForReNotifications = 0;
                                        }
                                    }
                                    temporizerForReNotifications++;
                                }

                            }
                            catch (Exception e)
                            {

                            }



                        }





                        if (IsRunning)
                            firstRun = false;


                    }
                }
                catch (Exception e)
                {

                }
            }, token);
        }
        List<int> guestQueueReportedNotifications = new List<int>();
        private static async Task NotifyPendingServices()
        {

            await CrossNotifications.Current.Send(new Notification
            {
                Date = DateTime.Now,
                Id = 1,
                Message = "Hay servicios en la zona que aun no han sido atendidos.",
                Title = "SanCristobal Gruas",
                Sound = "alertsound"
            });
            CrossNotifications.Current.Vibrate(3000);
            await Task.Delay(5000).ContinueWith(prevTask => { CrossNotifications.Current.Vibrate(2000); });
        }

        public static async Task NotifyWaitingForCraneAsignal()
        {

            await CrossNotifications.Current.Send(new Notification
            {
                Date = DateTime.Now,
                Id = 1,
                Message = "Esperando que se te asigne una grua.",
                Title = "SanCristobal Gruas",
                Sound = "alertsound"
            });
            CrossNotifications.Current.Vibrate(3000);
            await Task.Delay(5000).ContinueWith(prevTask => { CrossNotifications.Current.Vibrate(2000); });
        }

        public static async Task NotifyCraneTakenSolicitude()
        {

            await CrossNotifications.Current.Send(new Notification
            {
                Date = DateTime.Now,
                Id = 1,
                Message = "Una grua ya ha sido asignada a tu solicitud de soporte. Abre la aplicacion para ver el tiempo de llegada.",
                Title = "SanCristobal Gruas",
                Sound = "alertsound"
            });
            CrossNotifications.Current.Vibrate(3000);
            await Task.Delay(5000).ContinueWith(prevTask => { CrossNotifications.Current.Vibrate(2000); });
        }



        private static async Task RemoveNotification()
        {

            await CrossNotifications.Current.Cancel(1);
        }



        public static void StopService()
        {

            WriteServiceInfoToServiceFile(false, "","");

            MessagingCenter.Send(new StopLongRunningTaskMessage(),
                "StopLongRunningTaskMessage");
        }
    }
}
