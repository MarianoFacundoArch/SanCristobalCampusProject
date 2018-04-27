using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonCross.DTOS;
using Services;

namespace Backend.Controllers
{
    public class AccountsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login([FromBody] UserLoginDTO login)
        {
            var loginResult = new AccountServices().LoginUser(login.username, login.password);
            if (loginResult != null)
            {
                return Json(loginResult);
            }
            return this.NotFound();
        }


        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterUserDTO login)
        {
            var registerResult = new AccountServices().RegisterUser(login);
            if (registerResult != null)
            {
                return Json(registerResult);
                
            }
            return this.Conflict();
        }

    }
}
