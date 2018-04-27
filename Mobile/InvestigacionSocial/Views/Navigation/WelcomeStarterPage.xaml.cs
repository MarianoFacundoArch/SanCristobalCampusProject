using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InvestigacionSocial
{
	public partial class WelcomeStarterPage : ContentPage
	{
	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        welcomeTitleLabel.Text = GlobalData.myUser.first_name + " bienvenido a la app de San Cristobal Seguros";
        }

	    public WelcomeStarterPage()
		{
			InitializeComponent();

		}
	}
}
