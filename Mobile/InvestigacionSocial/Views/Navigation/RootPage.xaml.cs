using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.Services;
using InvestigacionSocial.ViewModels;
using InvestigacionSocial.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        private bool _isLoading;
        public RootPage(bool isLoading = true)
        {

                InitializeComponent();
                _isLoading = isLoading;

                // Empty pages are initially set to get optimal launch experience
                Master = new ContentPage { Title = "Picadito" };
                Detail = NavigationPageHelper.Create(new ContentPage());


        }

        public async void OnSettingsTapped(Object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SettingsPage());
        }

        protected async override void OnAppearing()
        {





            

            if (!LoginServices.isTokenDefinedForLogin())
            {
                Navigation.PushModalAsync(new LoginPage());

            }
            else
            {


                if (GlobalData.myUser == null)
                {
                    Navigation.PushModalAsync(new loginDataLoaderPage());
                }
                else
                {
                    MasterCoordinator.MenuSelected += MasterCoordinator_ItemSelected;



                    //if (App.GlobalDataInstance.MyUser != null)
                    //{


                    //if (!Application.Current.Properties.ContainsKey("StepByStep"))
                    //{
                    //    Navigation.PushModalAsync(new InitialWalkthrough());
                    //    Application.Current.Properties.Add("StepByStep", "DONE");
                    //}



                    if (_isLoading)
                    {
                        if (Detail.GetType() != typeof(WelcomeStarterPage))
                            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(WelcomeStarterPage)));

                        await Task.Delay(500)
                            .ContinueWith(t => NavigationService.BeginInvokeOnMainThreadAsync(InitializeMasterDetail));
                        _isLoading = false;

                    }



                    //}
                    /*else
                    {
                        Navigation.PushModalAsync(new SplashScreenPage());


                    }


        */

                }




            }


            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MasterCoordinator.MenuSelected -= MasterCoordinator_ItemSelected;
        }

        private void InitializeMasterDetail()
        {
            try
            {
                Master = new MainMenuPage(new NavigationService(Navigation, LaunchPageInDetail));
                Detail = NavigationPageHelper.Create(new WelcomeStarterPage());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void LaunchPageInDetail(Page page, bool animated)
        {

            Detail = NavigationPageHelper.Create(page);
            IsPresented = false;
        }

        private void MasterCoordinator_ItemSelected(object sender, SampleEventArgs e)
        {
            if (e.Menu.PageType == typeof(RootPage))
            {
                IsPresented = true;
            }
        }
    }
}