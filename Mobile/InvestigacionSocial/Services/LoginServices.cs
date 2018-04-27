using System;
using System.Collections.Generic;
using System.Text;
using CommonCross.UsefulFunctions;
using InvestigacionSocial.PlatformSpecific;
using Xamarin.Forms;

namespace InvestigacionSocial.Services
{
    public class LoginServices
    {
        public static void RemoveLoginDataAndCloseApp()
        {
            if (Application.Current.Properties.ContainsKey("loginInfo"))
                Application.Current.Properties.Remove("loginInfo");


            GlobalData.LoginData = null;
            DependencyService.Get<ICloseApplication>().CloseApp();

        }

        public static bool isTokenDefinedForLogin()
        {
            if (GlobalData.LoginData == null)
            {
                return false;
            }

            if (TimeFunctions.UnixTimeStampToDateTime(Convert.ToInt64(GlobalData.LoginData.expire_timestamp)) < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
