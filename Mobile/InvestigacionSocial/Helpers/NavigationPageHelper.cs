using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InvestigacionSocial.Helpers
{
    public static class NavigationPageHelper
    {
        public static NavigationPage Create(Page page)
        {
            return new NavigationPage(page) { BarTextColor = Color.White };
        }
    }
}
