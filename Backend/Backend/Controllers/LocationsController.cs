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
    public class LocationsController : ApiController
    {
        [HttpPost, UserAuthorization]
        public IHttpActionResult NearCranes([FromBody] CoordinateDTO coordinates)
        {
            var _userWithData = (RegisterUserDTO) HttpContext.Current.Items["user"];
            new LocationServices().UpdatePosition(_userWithData.user_id, coordinates);
            return Json(new LocationServices().GetNearCranes(coordinates));
        }

        [HttpPost, UserAuthorization]
        public IHttpActionResult NearUntakenServicesCount([FromBody] CoordinateDTO coordinates)
        {
            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];
            if (_userWithData.is_provider == 1)
            {
                new LocationServices().UpdatePosition(_userWithData.user_id, coordinates);
                return Json(new NearUntakenServicesCountDTO() { count = new LocationServices().GetNearUntakenServices(coordinates).Count});
            }
            return Conflict();
        }

        [HttpPost, CraneAuthorization]
        public IHttpActionResult NearUntakenServices([FromBody] CoordinateDTO coordinates)
        {
            var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];

                new LocationServices().UpdatePosition(_userWithData.user_id, coordinates);
                return Json(new LocationServices().GetNearUntakenServices(coordinates));
        }


        [HttpPost, UserAuthorization]
            public IHttpActionResult Update([FromBody] CoordinateDTO coordinates)
            {
                var _userWithData = (RegisterUserDTO)HttpContext.Current.Items["user"];
                new LocationServices().UpdatePosition(_userWithData.user_id, coordinates);
                return Json(new StatusChangeDTO(_userWithData.status));
            }
    }
}
