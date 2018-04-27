using Foundation;
using UIKit;
using Xamarin.Forms;

using Lottie.Forms.iOS.Renderers;

using FFImageLoading.Forms.Touch;

using UXDivers.Artina.Shared;
using System;
using InvestigacionSocial.BackgroundService;


namespace InvestigacionSocial.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
	    private iOSLongRunningTaskExample longRunningTaskExample;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			CachedImageRenderer.Init(); // Initializing FFImageLoading
			AnimationViewRenderer.Init(); // Initializing Lottie

			UXDivers.Artina.Shared.GrialKit.Init(new ThemeColors(), "InvestigacionSocial.iOS.GrialLicense");

#if !DEBUG
			// Reminder to update the project license to production mode before publishing
			if (!UXDivers.Artina.Shared.License.IsProductionLicense())
			{
				BeginInvokeOnMainThread(() =>
				{
					try
					{
						var alert = UIAlertController.Create(
							"Grial UI Kit Reminder",
							"Before publishing this App remember to change the license file to PRODUCTION MODE so it doesn't expire.",
							UIAlertControllerStyle.Alert);

						alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

						var root = UIApplication.SharedApplication.KeyWindow.RootViewController;
						var controller = root.PresentedViewController ?? root.PresentationController.PresentedViewController;
						controller.PresentViewController(alert, animated: true, completionHandler: null);
					}
					catch
					{
					}
				});
			}
#endif

			// Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif

			FormsHelper.ForceLoadingAssemblyContainingType(typeof(UXDivers.Effects.Effects));
			FormsHelper.ForceLoadingAssemblyContainingType<UXDivers.Effects.iOS.CircleEffect>();
			FormsHelper.ForceLoadingAssemblyContainingType<TabItem>();
			FormsHelper.ForceLoadingAssemblyContainingType<Repeater>();
			FormsHelper.ForceLoadingAssemblyContainingType<FFImageLoading.Transformations.BlurredTransformation>();

			ReferenceCalendars();


		    /*
            * Background Service starters
            * */

		    MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", async message => {
		        try
		        {
		            longRunningTaskExample = new iOSLongRunningTaskExample();
		            await longRunningTaskExample.Start();
		        }
		        catch (Exception ex)
		        {

		        }

		    });

		    MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message => {
		        try
		        {
		            if (longRunningTaskExample != null)
		                longRunningTaskExample.Stop();
		        }
		        catch (Exception e)
		        {

		        }

		    });



            LoadApplication(new App ());

			return base.FinishedLaunching(app, options);
		}

		private void ReferenceCalendars()
		{
			// When compiling in release, you may need to instantiate the specific
			// calendar so it doesn't get stripped out by the linker. Just uncomment
			// the lines you need according to the localization needs of the app.
			// For instance, in 'ar' cultures UmAlQuraCalendar is required.
			// https://bugzilla.xamarin.com/show_bug.cgi?id=59077

			new System.Globalization.UmAlQuraCalendar();
			// new System.Globalization.ChineseLunisolarCalendar();
			// new System.Globalization.ChineseLunisolarCalendar();
			// new System.Globalization.HebrewCalendar();
			// new System.Globalization.HijriCalendar();
			// new System.Globalization.IdnMapping();
			// new System.Globalization.JapaneseCalendar();
			// new System.Globalization.JapaneseLunisolarCalendar();
			// new System.Globalization.JulianCalendar();
			// new System.Globalization.KoreanCalendar();
			// new System.Globalization.KoreanLunisolarCalendar();
			// new System.Globalization.PersianCalendar();
			// new System.Globalization.TaiwanCalendar();
			// new System.Globalization.TaiwanLunisolarCalendar();
			// new System.Globalization.ThaiBuddhistCalendar();
		}
	}
}
