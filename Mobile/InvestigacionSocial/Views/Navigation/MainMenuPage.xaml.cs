using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestigacionSocial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Navigation
{
    public partial class MainMenuPage : ContentPage
    {
        private readonly INavigation _navigation;

        public MainMenuPage(INavigation navigation)
        {
            InitializeComponent();

            _navigation = navigation;

            BindingContext = new MasterDetailViewModel(navigation);
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var sample = sampleListView.SelectedItem as InvestigacionSocial.ViewModels.Menu;

            if (sample != null)
            {
                if (sample.PageType == typeof(RootPage))
                {
                    await DisplayAlert("Hey", string.Format("You are already here, on menu {0}.", sample.Name), "OK");
                }
                else
                {
                    await sample.NavigateToSample(_navigation);
                }

                sampleListView.SelectedItem = null;
            }
        }

        private async void OnCloseButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public void OnBtnCustomClicked()
        {
            //var uri = "mailto:hello@grialkit.com?subject=I%20want%20a%20custom%20theme%20for%20my%20Grial%20app&body=Please%20leave%20us%20your%20comments.";
            var uri = "http://grialkit.com/getquote";
            Device.OpenUri(new Uri(uri));
        }
    }
}