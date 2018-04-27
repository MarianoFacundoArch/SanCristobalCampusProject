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
    public class AccountServices
    {

        public UserTokenDTO RegisterUser(RegisterUserDTO userToRegister)
        {
            User addedUser = null;
            using (var entities = new UCAInvestDBEntities())
            {
                var dbResponse =
                    entities.users.SingleOrDefault(t => t.mail == userToRegister.mail || t.username==userToRegister.username);

                if (dbResponse == null)
                {

                    long creationTimeStamp = Convert.ToInt64(TimeFunctions.DateTimeToUnixTimestamp(DateTime.Now));
                    string salt = CryptoServices.GenerateGuid().Substring(0,8);
                    string password = CryptoServices.SHA256Encrypt(userToRegister.password, salt);
                    var userToAdd = new User()
                    {
                        first_name = userToRegister.first_name,
                        mail = userToRegister.mail,
                        last_name = userToRegister.last_name,
                        mobile_phone = userToRegister.mobile_phone,
                        creation_timestamp = creationTimeStamp,
                        password = password,
                        salt = salt,
                        username = userToRegister.username,
                        status = "freeuser"
                    };
                    addedUser = entities.users.Add(userToAdd);
                    entities.SaveChanges();
                }
            }

            if (addedUser!=null)
                return GiveMeUniqueAvailableToken(addedUser.user_id);

            return null;
        }


        public UserTokenDTO LoginUser(string username, string password)
        {
            UserTokenDTO response = null;
            using (var entities = new UCAInvestDBEntities())
            {

                var dbResponse =
                    entities.users.SingleOrDefault(t => t.username == username);
                if (dbResponse != null)
                {
                    string salt = dbResponse.salt;
                    if (dbResponse.password == CryptoServices.SHA256Encrypt(password,salt))
                        response = GiveMeUniqueAvailableToken(dbResponse.user_id);

                }
            }

            return response;
        }


        public UserTokenDTO GiveMeUniqueAvailableToken(int userId)
        {
            var GuidString = CryptoServices.GenerateToken();
            long expireTime =
                Convert.ToInt64(TimeFunctions.DateTimeToUnixTimestamp(
                    DateTime.Now.AddMinutes(GlobalConfig.TokenExpiresInMinutes)));

            using (var entities = new UCAInvestDBEntities())
            {
                var dbResponse = entities.user_tokens.FirstOrDefault(t => t.token == GuidString);
                if (dbResponse == null)
                {
                    var tokenToAdd = new UserToken()
                    {
                        user_id = userId,
                        expire_timestamp = expireTime,
                        token = GuidString
                    };

                    entities.user_tokens.Add(tokenToAdd);

                    entities.SaveChanges();

                    var config = new MapperConfiguration(cfg => {

                        cfg.CreateMap<UserToken, UserTokenDTO>();

                    });

                    IMapper iMapper = config.CreateMapper();

                    return iMapper.Map<UserToken, UserTokenDTO>(tokenToAdd);


                }
            }
            return GiveMeUniqueAvailableToken(userId);
        }
    }
}
