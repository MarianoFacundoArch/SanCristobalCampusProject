using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using InvestigacionSocial.Exceptions;
using InvestigacionSocial.PlatformSpecific;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnCloseButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        public static bool IsPageInNavigationStack<TPage>(INavigation navigation) where TPage : Page
        {
            if (navigation.NavigationStack.Count > 1)
            {
                var last = navigation.NavigationStack[navigation.NavigationStack.Count - 2];

                if (last is TPage)
                {
                    return true;
                }
            }
            return false;
        }

        private async void LoginButton_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Faltan datos", "Por favor complete los datos correctamente", "Continuar");
                return;

            }
            DisableVisualInteraction();
            try
            {
                var response = await BackendService.sendJsonPostData("Accounts/Login",
                    JsonConvert.SerializeObject(new UserLoginDTO { username = userEntry.Text, password = passwordEntry.Text }),
                    true);
                var LoginDetails = JsonConvert.DeserializeObject<UserTokenDTO>(response);
                GlobalData.LoginData = LoginDetails;
                Application.Current.Properties["loginInfo"] = response;
                Navigation.PopModalAsync();
            }
            catch (NotFoundException)
            {
                await DisplayAlert("Error", "Datos incorrectos.", "Continuar");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error", "Ha ocurrido un error, intente mas tarde", "Continuar");
            }
            EnableVisualInteraction();
        }

        private void EnableVisualInteraction()
        {
            loginButton.IsEnabled = true;
            passwordEntry.IsEnabled = true;
            userEntry.IsEnabled = true;
            indicatorLogin.IsVisible = false;
        }

        private void DisableVisualInteraction()
        {
            loginButton.IsEnabled = false;
            passwordEntry.IsEnabled = false;
            userEntry.IsEnabled = false;
            indicatorLogin.IsVisible = true;
        }


        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<ICloseApplication>().CloseApp();
            return false;
        }

	    private void RegisterButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushModalAsync(new RegisterPage());
	    }
	}
}