using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using InvestigacionSocial.BackgroundService;
using InvestigacionSocial.Exceptions;
using InvestigacionSocial.PlatformSpecific;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class loginDataLoaderPage : ContentPage
	{
		public loginDataLoaderPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            DoLogIn(0);
        }
        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<ICloseApplication>().CloseApp();
            return false;
            //return base.OnBackButtonPressed();
        }


        private async void DoLogIn(int tries)
        {
            try
            {

                nowLoadingLabel.Text = "Abriendo borradores...";

                PublicUserDTO _myUser =
                    JsonConvert.DeserializeObject<PublicUserDTO>(await BackendService.sendGet("Users/MyDetails"));
                GlobalData.myUser = _myUser;

                if (_myUser.is_provider==1)
                {
                    LocationService.StartServiceInCorrespondingPlatformIfNotRunning();
                }
                if (_myUser.status == "waiting_crane_arrival")
                {
                    LocationService.StartServiceInCorrespondingPlatformIfNotRunning();
                    LocationService.NotifyCraneTakenSolicitude();
                }

                if (_myUser.status == "service_requested")
                    {
                        LocationService.StartServiceInCorrespondingPlatformIfNotRunning();
                        LocationService.NotifyWaitingForCraneAsignal();
                    }
                    /*nowLoadingLabel.Text = "Pidiendo pancitos...";
                    string _myDetails = await BackendService.sendGet("users/MyDetails");
                    UserDTO loadedUser = JsonConvert.DeserializeObject<UserDTO>(_myDetails);
                    GlobalData.MyUser = loadedUser;
                    nowLoadingLabel.Text = "Calentando jugadores...";
                    NotificationRegisterDTO notifRegister = new NotificationRegisterDTO();
                    notifRegister.OneSignalID = App.GlobalDataInstance.OneSignalUSERID;
                    var responseNotif = await BackendService.sendJsonPostData("users/SubscribeNotifications", JsonConvert.SerializeObject(notifRegister));
                    */
                    Navigation.PopModalAsync();

            }
            catch (NotFoundException e)
            {
                GlobalData.LoginData = null;
                Navigation.PopModalAsync();
                return;
            }
            catch (UnauthorizedException e)
            {
                GlobalData.LoginData = null;
                Navigation.PopModalAsync();
                return;
            }
            catch (System.Exception e)
            {
                if (tries < 100)
                {
                    nowLoadingLabel.Text = "Partido suspendido... Esperando para re intentar.";
                    await CommonCross.UsefulFunctions.TimeFunctions.Foo(2000);
                    DoLogIn(++tries);
                }
                else
                {
                    await DisplayAlert("Error de sesion",
                "Ha habido un error fatal en la conexion. Intenta mas tarde.", "Ok");
                    var closer = DependencyService.Get<ICloseApplication>();
                    if (closer != null)
                        closer.CloseApp();
                }
                return;
            }
        }

    }
}