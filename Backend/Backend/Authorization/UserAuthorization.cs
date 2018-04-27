using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Services;

namespace Backend.Authorization
{
    public class UserAuthorization : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnAuthorizationAsync(actionContext, cancellationToken);
        }

        private const string _securityToken = "token"; // Name of the url parameter.

        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {

                var getParams = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
                string token = getParams[_securityToken];
                var restoUser = new UsersServices().UserForToken(token);
                HttpContext.Current.Items["token"] = token;
                if (restoUser != null)
                {
                    HttpContext.Current.Items["user"] = restoUser;

                    return true;
                }

                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}