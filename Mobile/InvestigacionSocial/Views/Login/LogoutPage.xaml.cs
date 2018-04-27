using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestigacionSocial.BackgroundService;
using InvestigacionSocial.PlatformSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutPage : ContentPage
	{
		public LogoutPage ()
		{
			InitializeComponent ();
		    Application.Current.Properties.Remove("loginInfo");
            LocationService.StopService();
            DependencyService.Get<ICloseApplication>().CloseApp();
        }
	}
}