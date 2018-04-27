using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using InvestigacionSocial.BackgroundService;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.Services;
using InvestigacionSocial.Views.Services;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mapPage : ContentPage
    {
        private Timer myTimer2;
        private bool timerShouldRun = false;

        private static void Callback(object state)
        {
            if (((mapPage) state).myTimer2 == null)
                return;
            ((mapPage) state).timerElapsed();
        }

        protected override void OnAppearing()
        {

            if (GlobalData.myUser.status == "service_requested")
            {
                Navigation.PushAsync(new ServiceWaitingPickUpPage());
                LocationService.NotifyWaitingForCraneAsignal();
                LocationService.StartServiceInCorrespondingPlatformIfNotRunning();
                return;
            }

            if (GlobalData.myUser.status == "waiting_crane_arrival")
            {
                Navigation.PushAsync(new ServiceTakenMapPage());
                LocationService.NotifyWaitingForCraneAsignal();
                LocationService.StartServiceInCorrespondingPlatformIfNotRunning();
                return;
            }

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
	            var myPosition = GeoLocatorService.GetCurrentPosition();
                List<MapPinDTO> cranes =
	                JsonConvert.DeserializeObject<List<MapPinDTO>>(BackendService.sendJsonPostData("Locations/NearCranes",JsonConvert.SerializeObject(new CoordinateDTO(myPosition.Result.Latitude,myPosition.Result.Longitude))).Result);
	           
	            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	            {
	                DrawCranes(cranes);
	            });
	        }
	        catch (Exception exception)
	        {

	        }


	    }

        private void DrawCranes(List<MapPinDTO> cranes)
        {
            MyMap.Pins.Clear();
            foreach (var crane in cranes)
            {
                Position position = new Position(crane.coordinates.latitude, crane.coordinates.longitude);
                string lastSeen = TimeFunctions.UnixTimeStampToDateTime(crane.last_seen_timestamp).ToShortTimeString();
                Pin pin = new Pin()
                {
                    Label = "Grua: " + crane.name,
                    Address = "Ultima vez visto: " + lastSeen,
                    Position = position,
                };
                pin.Icon = BitmapDescriptorFactory.FromBundle("grua.png");
                MyMap.Pins.Add(pin);
            }
        }
        public mapPage ()
		{
			InitializeComponent ();
		    try
		    {
                MyMap.MyLocationEnabled = true;
		        MyMap.IsTrafficEnabled = true;
		        var myPosition = GeoLocatorService.GetCurrentPosition();

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(myPosition.Result.Latitude, myPosition.Result.Longitude), Distance.FromKilometers(7)));

            }
		    catch (Exception e)
		    {
		        Console.WriteLine(e);

		    }
		    

		}

        private void RequestButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RequestServicePage());
        }
    }
}