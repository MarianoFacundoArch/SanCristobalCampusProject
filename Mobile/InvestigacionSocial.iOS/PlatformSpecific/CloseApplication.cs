using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InvestigacionSocial.iOS.PlatformSpecific;
using InvestigacionSocial.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace InvestigacionSocial.iOS.PlatformSpecific
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
