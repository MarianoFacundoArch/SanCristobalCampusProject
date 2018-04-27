using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.Threading.Tasks;

using UXDivers.Artina.Shared;

using FFImageLoading.Forms.Droid;
using InvestigacionSocial.BackgroundService;
using Java.Util;
using Plugin.Permissions;


namespace InvestigacionSocial.Droid
{
	//https://developer.android.com/guide/topics/manifest/activity-element.html
	[Activity(
		Label = "Gruas San Cristobal",
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
		MainLauncher = true,
		LaunchMode = LaunchMode.SingleTask,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Locale | ConfigChanges.LayoutDirection
		)
	]
	public class MainActivity : FormsAppCompatActivity
	{
		private Locale _locale;

		protected override void OnCreate(Bundle bundle)
		{
			// Changing to App's theme since we are OnCreate and we are ready to 
			// "hide" the splash
			base.Window.RequestFeature(WindowFeatures.ActionBar);
			base.SetTheme(Resource.Style.AppTheme);

			FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
			FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabs;

			base.OnCreate(bundle);

			// Initializing FFImageLoading
			CachedImageRenderer.Init(enableFastRenderer: false);

			global::Xamarin.Forms.Forms.Init(this, bundle);
		    Xamarin.FormsGoogleMaps.Init(this, bundle);
            UXDivers.Artina.Shared.GrialKit.Init(this, "InvestigacionSocial.Droid.GrialLicense");

#if !DEBUG
			// Reminder to update the project license to production mode before publishing
			if (!UXDivers.Artina.Shared.License.IsProductionLicense())
			{
				try
				{
					AlertDialog.Builder alert = new AlertDialog.Builder(this);
					alert.SetTitle("Grial UI Kit Reminder");
					alert.SetMessage("Before publishing this App remember to change the license file to PRODUCTION MODE so it doesn't expire.");
					alert.SetPositiveButton("OK", (sender, e) => { });

					var dialog = alert.Create();
					dialog.Show();
				}
				catch
				{
				}
			}
#endif

			FormsHelper.ForceLoadingAssemblyContainingType(typeof(UXDivers.Effects.Effects));

			_locale = Resources.Configuration.Locale;

			ReferenceCalendars();



		    /*
            * Suscribe to background service
            * */
		    MessagingCenter.Subscribe<StartLongRunningTaskMessage>(this, "StartLongRunningTaskMessage", message =>
		    {

		        try
		        {
		            var intent = new Intent(this, typeof(LongRunningTaskService));
		            StartService(intent);
		        }
		        catch (Exception e)
		        {

		        }




		    });

		    MessagingCenter.Subscribe<StopLongRunningTaskMessage>(this, "StopLongRunningTaskMessage", message =>
		    {
		        try
		        {
		            var intent = new Intent(this, typeof(LongRunningTaskService));
		            StopService(intent);
		        }
		        catch (Exception e)
		        {

		        }


		    });


            LoadApplication(new App());
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
			base.OnConfigurationChanged(newConfig);

			GrialKit.NotifyConfigurationChanged(newConfig);

			if ((int)Build.VERSION.SdkInt <= 19 &&
			    !_locale.Equals(newConfig.Locale))
			{
				// Need to recreate the activity when locale has changed for APIs 18 and 19 
				// as changes in ConfigChanges.Locale brake images used in the app
				Recreate();
			}
        }

	    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
	    {
	        PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

