using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestigacionSocial.BackgroundService;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.PlatformSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServiceWaitingPickUpPage : ContentPage
	{
	    private Timer myTimer2;
	    private bool timerShouldRun = false;
	    private static void Callback(object state)
	    {
	        if (((ServiceWaitingPickUpPage)state).myTimer2 == null)
	            return;
	        ((ServiceWaitingPickUpPage)state).timerElapsed();
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

	    private bool closed = false;

	    private string[] tips = new[]
	    {
	        "En caso de incendio utilizá el matafuego de los vehículos.",
	        "Inmovilizá el vehículo parando el motor y accionando el freno de mano.",
	        "En el lugar del accidente; No fumes, enciendas fósforos o encendedores. Tampoco permitas que los otros lo hagan.",
	        "Colocar los triángulos baliza en ambos sentidos de circulación (salvo que sea autovía o autopista) para avisar a los otros conductores. Tené en cuenta las distancias prudentes para colocarlos.",
	        "Dejá una vía libre para los servicios de emergencia, poné balizas y si podés ponete un chaleco reflectante (deberías tenerlo en el auto)."
	    };

	    private int timerTips = 0;

	    private void setRandomTip()
	    {
	        labelTip.Text = tips[new Random().Next(0, tips.Count() - 1)];

	    }
	    private void timerElapsed()
	    {
	        try
	        {
	            timerTips++;
                if (timerTips == 15)

	            {
	                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	                {
	                    setRandomTip();


	                });
	                timerTips = 0;
	            }
	            if (GlobalData.myUser.status != "service_requested")
	                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	                {
	                    if (!closed)
	                    {
	                        closed = true;
	                        Navigation.PopAsync();
                        }
	                    
	                });
            }
	        catch (Exception exception)
	        {

	        }


	    }

        protected override void OnAppearing()
	    {
	        try
	        {

	            setRandomTip();
                timerShouldRun = true;
	            if (myTimer2 == null || myTimer2.IsCancellationRequested)
	                myTimer2 = new Timer(Callback, this, 3000, 1000, true);
	        }
	        catch (Exception e)
	        {

	        }
	        base.OnAppearing();

	    }


        public ServiceWaitingPickUpPage ()
		{
		    NavigationPage.SetHasBackButton(this, false);
            InitializeComponent ();
		}

	    protected override bool OnBackButtonPressed()
	    {
	        DependencyService.Get<ICloseApplication>().CloseApp();
	        return false;
	    }

    }
}