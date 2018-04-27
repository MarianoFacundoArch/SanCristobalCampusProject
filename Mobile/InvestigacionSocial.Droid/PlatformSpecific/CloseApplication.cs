using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InvestigacionSocial.Droid.PlatformSpecific;
using InvestigacionSocial.PlatformSpecific;
using Xamarin.Forms;
[assembly: Dependency(typeof(CloseApplication))]
namespace InvestigacionSocial.Droid.PlatformSpecific
{

        public class CloseApplication : ICloseApplication
        {
            public void CloseApp()
            {
                var activity = (Activity)Forms.Context;
                activity.FinishAffinity();
            }
        }

}