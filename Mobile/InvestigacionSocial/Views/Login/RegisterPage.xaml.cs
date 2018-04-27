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
	public partial class RegisterPage : ContentPage
	{
        public RegisterPage()
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



        private void EnableVisualInteraction()
        {
            passwordEntry.IsEnabled = true;
            emailEntry.IsEnabled = true;
            indicatorLogin.IsVisible = false;
        }

        private void DisableVisualInteraction()
        {
            passwordEntry.IsEnabled = false;
            emailEntry.IsEnabled = false;
            indicatorLogin.IsVisible = true;
        }


        protected override bool OnBackButtonPressed()
        {
            DependencyService.Get<ICloseApplication>().CloseApp();
            return false;
        }

        private async void RegisterButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text) ||
                    string.IsNullOrEmpty(userEntry.Text) || string.IsNullOrEmpty(firstNameEntry.Text) ||
                    string.IsNullOrEmpty(lastNameEntry.Text))
                {
                    DisplayAlert("Faltan datos", "Por favor complete los datos correctamente", "Continuar");
                    return;

                }
                DisableVisualInteraction();

                var response = await BackendService.sendJsonPostData("Accounts/Register",
                    JsonConvert.SerializeObject(new RegisterUserDTO()
                    {
                        mail = emailEntry.Text,
                        password = passwordEntry.Text,
                        first_name = firstNameEntry.Text,
                        last_name = lastNameEntry.Text,
                        mobile_phone = mobilePhoneEntry.Text,
                        username = userEntry.Text,
                        plate = plateEntry.Text
                    }),
                    true);
                var LoginDetails = JsonConvert.DeserializeObject<UserTokenDTO>(response);
                GlobalData.LoginData = LoginDetails;
                Application.Current.Properties["loginInfo"] = response;
                Navigation.PopModalAsync();

            }
            catch (ConflictException conflict)
            {
                await DisplayAlert("Error", "Ya existe una cuenta con ese email.", "Continuar");
            }
            catch (Exception exception)
            {
                await DisplayAlert("Error", "Ha ocurrido un error, intente mas tarde", "Continuar");
            }
            finally
            {
                EnableVisualInteraction();
            }
            
        }
    }
}