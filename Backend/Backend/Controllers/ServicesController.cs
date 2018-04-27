using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Backend.Authorization;
using CommonCross.DTOS;
using Services;

namespace Backend.Controllers
{
    public class ServicesController : ApiController
    {
        [HttpPost, UserAuthorization]
        public IHttpActionResult Request([FromBody] ServiceRequestDTO request)
        {
            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];;
            if (new ServicesServices().RequestService(_userWithData.user_id, request))
            {
                return Json(new StatusChangeDTO("service_requested"));
            }
            return Conflict();
        }


        [HttpGet, CraneAuthorization]
        public IHttpActionResult TakeRequest(int id)
        {
            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"]; ;
            if (new ServicesServices().TakeService(_userWithData.user_id, id))
            {
                return Json(new StatusChangeDTO("unavailable_crane"));
            }
            return Conflict();
        }


        [HttpPost, UserAuthorization]
        public IHttpActionResult ServicePosition([FromBody] CoordinateDTO coordinates)
        {

            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];
            new LocationServices().UpdatePosition(_userWithData.user_id, coordinates);
            return Json(new ServicesServices().ServicePosition(_userWithData.user_id));
        }


        [HttpGet, UserAuthorization]
        public IHttpActionResult Finish(int id)
        {

            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];
            new ServicesServices().FinishService(_userWithData.user_id, id);
            if (_userWithData.is_provider == 1)
            {
                return Json(new StatusChangeDTO("available_crane"));
            }
            else
            {
                return Json(new StatusChangeDTO("freeuser"));
            }
        }

    }
}
