using CommonCross.DTOS;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestServicePage : ContentPage
	{
		public RequestServicePage ()
		{
			InitializeComponent ();
		    var myPosition = GeoLocatorService.GetCurrentPosition();

		    requestMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(myPosition.Result.Latitude, myPosition.Result.Longitude), Distance.FromMeters(700)));
		    requestMap.Pins.Add(new Pin(){IsDraggable = true, Type = PinType.Place, Label = "Posicion  en la que solicitar el servicio", Address = "Posicion a la que llamar a la grua",Position = new Position(myPosition.Result.Latitude, myPosition.Result.Longitude)});
		    requestMap.MyLocationEnabled = true;


		}

	    private void DesignUI(bool Working)
	    {
	        descriptionEntry.IsEnabled = !Working;
	        requestButton.IsEnabled = !Working;
	        indicatorWorking.IsVisible = Working;
	        requestMap.IsEnabled = !Working;
	    }
	    private async void RequestButton_OnClicked(object sender, EventArgs e)
	    {
	        try
	        {

                DesignUI(true);
	            ServiceRequestDTO request = new ServiceRequestDTO(
	                new CoordinateDTO(requestMap.Pins.FirstOrDefault().Position.Latitude,
	                    requestMap.Pins.FirstOrDefault().Position.Longitude), descriptionEntry.Text);
	            var content = JsonConvert.DeserializeObject<StatusChangeDTO>(await BackendService.sendJsonPostData("Services/Request", JsonConvert.SerializeObject(request)));
	            if (content.status == "service_requested")
	            {
	                GlobalData.myUser.status = content.status;
	                Navigation.PopAsync();
	            }
	            else
	                throw new Exception();
	        }
	        catch (Exception exception)
	        {
	            DisplayAlert("Error", "Error al solicitar el servicio. Reintente mas tarde.", "Continuar.");
	        }
	        finally
	        {
	            DesignUI(false);
	        }
	       



	    }
	}
}