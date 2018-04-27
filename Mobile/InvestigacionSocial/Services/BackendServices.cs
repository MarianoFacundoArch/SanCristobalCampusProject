using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InvestigacionSocial.Exceptions;

namespace InvestigacionSocial.Services
{
    public class BackendService
    {
        private static string _backendBase = CommonCross.CommonConfig.BackendBase;

        public static async Task<string> sendGet(string apiCall, string token)
        {
            try
            {
                HttpClient clientRequest = new HttpClient();
                //clientRequest.Timeout = TimeSpan.FromSeconds(GlobalData.TimeoutWebRequests);
                string queryUrl = _backendBase + apiCall + "?token=" + token;

                var contents = await clientRequest.GetStringAsync(queryUrl);


                return contents;
            }
            catch (System.Exception e)
            {
                if (e.Message == "401 (Unauthorized)")
                    throw new UnauthorizedException();
                if (e.Message == "404 (Not Found)")
                    throw new NotFoundException();
            }
            return null;

        }

        public static async Task<string> sendGet(string apiCall, bool AllowUnlogged = false)
        {
            try
            {
                HttpClient clientRequest = new HttpClient();
                //clientRequest.Timeout = TimeSpan.FromSeconds(GlobalData.TimeoutWebRequests);
                string queryUrl = "";
                if (!LoginServices.isTokenDefinedForLogin() && !AllowUnlogged)
                    throw new UnauthorizedException();



                if (!LoginServices.isTokenDefinedForLogin())
                {
                    queryUrl = _backendBase + apiCall;
                }
                else
                {
                    queryUrl = _backendBase + apiCall + "?token=" + GlobalData.LoginData.token;
                }
                var contents = await clientRequest.GetStringAsync(queryUrl);


                return contents;
            }
            catch (System.Exception e)
            {
                if (e.Message == "401 (Unauthorized)")
                    throw new UnauthorizedException();
                if (e.Message == "404 (Not Found)")
                    throw new NotFoundException();
            }
            return null;

        }


        public static async Task<string> sendJsonPostData(string apiCall, string JsonData, bool AllowUnlogged = false)
        {
            try
            {
                HttpClient clientRequest = new HttpClient();
                //clientRequest.Timeout = TimeSpan.FromSeconds(GlobalData.TimeoutWebRequests);
                var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                string queryUrl = "";

                if (!LoginServices.isTokenDefinedForLogin() && !AllowUnlogged)
                    throw new UnauthorizedException();

                if (!LoginServices.isTokenDefinedForLogin())
                {
                    queryUrl = _backendBase + apiCall;
                }
                else
                {
                    queryUrl = _backendBase + apiCall + "?token=" + GlobalData.LoginData.token;
                }
                var result = await clientRequest.PostAsync(queryUrl, content);
                var contents = await result.Content.ReadAsStringAsync();


                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new System.Exception(Convert.ToInt32(result.StatusCode) + " (" + result.ReasonPhrase + ")");
                }
                return contents;
            }
            catch (System.Exception e)
            {
                if (e.Message == "401 (Unauthorized)")
                    throw new UnauthorizedException();
                if (e.Message == "404 (Not Found)")
                    throw new NotFoundException();
                if (e.Message == "409 (Conflict)")
                    throw new ConflictException();

                throw e;
            }

        }

    }
}
