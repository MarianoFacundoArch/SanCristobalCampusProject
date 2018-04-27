using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.PlatformSpecific;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using Plugin.MapsPlugin;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServiceTakenMapPage : ContentPage
	{
	    private Timer myTimer2;
	    private bool timerShouldRun = false;
	    private static void Callback(object state)
	    {
	        if (((ServiceTakenMapPage)state).myTimer2 == null)
	            return;
	        ((ServiceTakenMapPage)state).timerElapsed();
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

	    private void timerElapsed()
	    {
	        try
	        {


	            if (!timerShouldRun)
	            {
	                if (!myTimer2.IsCancellationRequested)
	                    myTimer2.Dispose();
	                myTimer2 = null;
	                return;
	            }

	            if (GlobalData.myUser.status != "waiting_crane_arrival" && GlobalData.myUser.status != "unavailable_crane")
	            {
	                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	                {
	                    DoActionsBasedOnStatus();
	                });

                }

	            var myPosition = GeoLocatorService.GetCurrentPosition();
	            CurrentServicePositionsDTO currentPositions =
	                JsonConvert.DeserializeObject<CurrentServicePositionsDTO>(BackendService.sendJsonPostData("Services/ServicePosition", JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude, myPosition.Result.Longitude))).Result);

	            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	            {
	                DrawPositions(currentPositions);
	            });
            }
            catch (Exception exception)
	        {

	        }


	    }

        private bool mapAlreadyOpened = false;

	    private int serviceId;
	    private string phoneNumber;
	    private void DrawPositions(CurrentServicePositionsDTO currentPositions)
	    {

            if (!mapAlreadyOpened)
	        {

	            mapAlreadyOpened = true;

	            if (GlobalData.myUser != null && GlobalData.myUser.is_provider == 1)
	            {
	                Task.Delay(6000).ContinueWith(task =>
	                {
	                    CrossMapsPlugin.Current.PinTo("Vehículo averiado", currentPositions.RequestedPosition.latitude,
	                        currentPositions.RequestedPosition.longitude, 8);
	                });

	            }

	        }



	        if (currentPositions != null)
	        {
	            indicatorWorking.IsVisible = false;
	            serviceId = currentPositions.ServiceId;
	            phoneNumber = currentPositions.OtherUser.mobile_phone;
	            endButton.IsEnabled = true;
	            contactButton.IsEnabled = true;
                MyMap.Pins.Clear();
	            Position otherUserPosition = new Position(currentPositions.OtherUserPosition.latitude, currentPositions.OtherUserPosition.longitude);
	            Position brokenPosition = new Position(currentPositions.RequestedPosition.latitude, currentPositions.RequestedPosition.longitude);
	            string brokenLabel = "";

	            Pin otherUserPin = new Pin()
	            {
	                Label = currentPositions.OtherUser.username,
	                Address = "Patente: " + currentPositions.OtherUser.plate,
	                Position = otherUserPosition,
	            };

	            if (GlobalData.myUser != null && GlobalData.myUser.is_provider == 1)
	            {
	                brokenLabel = "Patente: " + currentPositions.OtherUser.plate;
	                otherUserPin.Icon = BitmapDescriptorFactory.FromBundle("persona.png");
	            }
	            else
	            {
	                brokenLabel = "Lugar al que acudira la grua";
	                otherUserPin.Icon = BitmapDescriptorFactory.FromBundle("grua.png");
	            }


	            Pin brokenPin = new Pin()
	            {
	                Label = "Posicion de la averia",
	                Address = brokenLabel,
	                Position = brokenPosition,
	            };
	            brokenPin.Icon = BitmapDescriptorFactory.FromBundle("autoservice.png");

	            MyMap.Pins.Add(brokenPin);
	            MyMap.Pins.Add(otherUserPin);
	            etaLabel.Text = "Tiempo estimado de arribo: " + currentPositions.TimeRemaining;
	            phoneLabel.Text = "Teléfono: " + currentPositions.OtherUser.mobile_phone;
	            if (GlobalData.myUser != null && GlobalData.myUser.is_provider == 1)
	            {
	                nameLabel.Text = "Nombre del cliente: " + currentPositions.OtherUser.first_name;
	            }
	            else
	            {
	                nameLabel.Text = "Nombre del prestador: " + currentPositions.OtherUser.first_name;
                }
	            
	            plateLabel.Text = "Patente: " + currentPositions.OtherUser.plate;


            }

        }

        protected override void OnAppearing()
	    {
	        try
	        {


	            timerShouldRun = true;
	            if (myTimer2 == null || myTimer2.IsCancellationRequested)
	                myTimer2 = new Timer(Callback, this, 3000, 1000, true);
	        }
	        catch (Exception e)
	        {

	        }
	        base.OnAppearing();

	    }
	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<ICloseApplication>().CloseApp();
	        return false;
	    }


        public ServiceTakenMapPage ()
		{
		    InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
		    MyMap.MyLocationEnabled = true;
		    MyMap.IsTrafficEnabled = true;
		    var myPosition = GeoLocatorService.GetCurrentPosition();

		    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(myPosition.Result.Latitude, myPosition.Result.Longitude), Distance.FromKilometers(7)));


		}

	    private bool closed = false;

        private void DoActionsBasedOnStatus()
	    {


            if (GlobalData.myUser != null && GlobalData.myUser.status != "unavailable_crane" &&
	            GlobalData.myUser.status != "waiting_crane_arrival")
	        {
	            if (!closed)
	            {
	                closed = true;
	                Navigation.PopAsync();
	            }
            }

        }
        private async void EndButton_OnClicked(object sender, EventArgs e)
	    {
	        try
	        {
                indicatorWorking.IsVisible = true;
	            endButton.IsEnabled = false;
	            contactButton.IsEnabled = false;
	            var content = JsonConvert.DeserializeObject<StatusChangeDTO>(
	                await BackendService.sendGet("Services/Finish/" + serviceId));
	            if (content.status == "freeuser" || content.status == "available_crane")
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

	            DisplayAlert("Error", "Error al finalizar el servicio. Reintente mas tarde.", "Continuar.");
	        }

	        finally
	        {
	            indicatorWorking.IsVisible = false;
                endButton.IsEnabled = true;
	            contactButton.IsEnabled = true;
	        }
        }

	    private void ContactButton_OnClicked(object sender, EventArgs e)
	    {
	        
            Device.OpenUri(new Uri(String.Concat("tel:",phoneNumber)));
	    }
	}
}
