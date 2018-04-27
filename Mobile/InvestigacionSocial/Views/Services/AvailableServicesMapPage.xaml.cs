using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using Plugin.MapsPlugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvailableServicesMapPage : ContentPage
    {
        private Timer myTimer2;
        private bool timerShouldRun = false;

        private static void Callback(object state)
        {
            if (((AvailableServicesMapPage) state).myTimer2 == null)
                return;
            ((AvailableServicesMapPage) state).timerElapsed();
        }

        private void DoActionsBasedOnStatus()
        {
            if (GlobalData.myUser!=null && GlobalData.myUser.status == "unavailable_crane")
            {
                Navigation.PushAsync(new ServiceTakenMapPage());

            }
        }
        protected override void OnAppearing()
        {

            
            try
            {
                DoActionsBasedOnStatus();
                Task.Factory.StartNew(() => { timerElapsed(true); });
                timerShouldRun = true;
                if (myTimer2 == null || myTimer2.IsCancellationRequested)
                    myTimer2 = new Timer(Callback, this, 60000, 60000, true);
            }
            catch (Exception e)
            {

            }
            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                timerShouldRun = false;
                if (myTimer2 != null)
                {
                    if (!myTimer2.IsCancellationRequested)
                        myTimer2.Dispose();
                    myTimer2 = null;

                }
            }
            catch (Exception e)
            {

            }


        }


        private void DrawServices(List<ServicePinDTO> services)
        {
            descriptionLabel.Text = "";
            requestButton.IsEnabled = false;
            selectedItem = null;
            MyMap.Pins.Clear();
            foreach (var crane in services)
            {
                Position position = new Position(crane.coordinates.latitude, crane.coordinates.longitude);
                string lastSeen = TimeFunctions.UnixTimeStampToDateTime(crane.last_seen_timestamp).ToShortTimeString();
                Pin pin = new Pin()
                {
                    Label = "Usuario: " + crane.name + "(Pat: " + crane.plate + ")",
                    Tag = crane,
                    Address = "Horario pedido: " + lastSeen,
                    Position = position,
                };
                pin.Icon = BitmapDescriptorFactory.FromBundle("autoservice.png");
                MyMap.Pins.Add(pin);
            }
        }

        private void timerElapsed(bool executeWithoutTimer = false)
        {
            try
            {
                if (!executeWithoutTimer && !timerShouldRun)
                {
                    if (!myTimer2.IsCancellationRequested)
                        myTimer2.Dispose();
                    myTimer2 = null;
                    return;
                }
                var myPosition = GeoLocatorService.GetCurrentPosition();
                List<ServicePinDTO> services =
                    JsonConvert.DeserializeObject<List<ServicePinDTO>>(BackendService
                        .sendJsonPostData("Locations/NearUntakenServices",
                            JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude,
                                myPosition.Result.Longitude))).Result);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    DrawServices(services);
                });
            }
            catch (Exception exception)
            {

            }


        }


        public AvailableServicesMapPage()
        {
            InitializeComponent();
            try
            {
                MyMap.MyLocationEnabled = true;
                MyMap.IsTrafficEnabled = true;
                MyMap.PinClicked += MyMapOnPinClicked;
                var myPosition = GeoLocatorService.GetCurrentPosition();

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.GoogleMaps.Position(myPosition.Result.Latitude, myPosition.Result.Longitude),
                    Distance.FromKilometers(7)));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        private ServicePinDTO selectedItem;

        private void MyMapOnPinClicked(object sender, PinClickedEventArgs pinClickedEventArgs)
        {
            var serviceSelected = (ServicePinDTO) pinClickedEventArgs.Pin.Tag;
            if (serviceSelected != null)
            {
                selectedItem = serviceSelected;
                descriptionLabel.Text = "Avería: " + serviceSelected.otherDetails;
                requestButton.IsEnabled = true;

            }
            else
            {
                descriptionLabel.Text = "";
                requestButton.IsEnabled = false;
                selectedItem = null;
            }

        }

        private async void RequestButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                DesignUI(true);
                var content = JsonConvert.DeserializeObject<StatusChangeDTO>(
                    await BackendService.sendGet("Services/TakeRequest/" + selectedItem.service_id));
                if (content.status == "unavailable_crane")
                {
                    GlobalData.myUser.status = content.status;
                    DoActionsBasedOnStatus();
                }
                else
                    throw new Exception();
            }
            catch
                (Exception exception)
            {

                DisplayAlert("Error", "Error al solicitar el servicio. Reintente mas tarde.", "Continuar.");
            }

            finally
            {

                DesignUI(false);
            }

        }

            private void DesignUI(bool Working)
        {
            requestButton.IsEnabled = !Working;
            indicatorWorking.IsVisible = Working;
            MyMap.IsEnabled = !Working;
        }
    }
}
