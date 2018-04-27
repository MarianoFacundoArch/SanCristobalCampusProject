using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CommonCross.DTOS;
using CommonCross.UsefulFunctions;
using DataSqlServer;

namespace Services
{
    public class UsersServices
    {

        public RegisterUserDTO UserForToken(string token)
        {
            RegisterUserDTO response = null;
            using (var entities = new UCAInvestDBEntities())
            {
                var dbResponse = entities.user_tokens.SingleOrDefault(t => t.token == token);
                if (dbResponse != null)
                {
                    var expireTime = TimeFunctions.UnixTimeStampToDateTime(Convert.ToInt64(dbResponse.expire_timestamp));

                    if (expireTime < DateTime.Now) // Token expired, has to delete
                    {
                        entities.user_tokens.Remove(dbResponse);
                        entities.SaveChanges();
                    }
                    else
                    {
                        var userLinked = dbResponse.users;
                        var config = new MapperConfiguration(cfg => {

                            cfg.CreateMap<User, RegisterUserDTO>();

                        });

                        IMapper iMapper = config.CreateMapper();

                        response = iMapper.Map<User, RegisterUserDTO>(userLinked);

                    }
                }
            }

            return response;
        }
        public PublicUserDTO PublicUserById(int id)
        {
            PublicUserDTO response = null;
            using (var entities = new UCAInvestDBEntities())
            {
                var dbResponse = entities.users.SingleOrDefault(t => t.user_id == id);
                if (dbResponse != null)
                {
                    var config = new MapperConfiguration(cfg => {

                        cfg.CreateMap<User, PublicUserDTO>();

                    });

                    IMapper iMapper = config.CreateMapper();

                    response = iMapper.Map<User, PublicUserDTO>(dbResponse);


                }
            }

            return response;
        }


    }
}
