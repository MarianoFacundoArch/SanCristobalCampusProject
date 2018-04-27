using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Backend.Authorization;
using CommonCross.DTOS;
using DataSqlServer;
using Services;

namespace Backend.Controllers
{
    public class UsersController : ApiController
    {


        // GET: api/Users/5


        [HttpGet, UserAuthorization]
        public IHttpActionResult MyDetails()
        {
            if (HttpContext.Current.Items.Contains("user"))
            {
                var _userWithData = (RegisterUserDTO) HttpContext.Current.Items["user"];
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<RegisterUserDTO, PublicUserDTO>();

                });

                IMapper iMapper = config.CreateMapper();

                return Json(iMapper.Map<RegisterUserDTO, PublicUserDTO>(_userWithData));
            }
            return NotFound();
        }


        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            return Json((new UsersServices()).PublicUserById(id));
        }





        
    }
}
