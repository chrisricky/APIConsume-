using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAPIConsume.AuthClass;
using NewAPIConsume.ExternalAPIs;
using NewAPIConsume.RequestObject;
using NewAPIConsume.ResponseObject;

namespace NewAPIConsume.Controllers
{
    public class NewUserOperationsController : ControllerBase
    {
        private IConfiguration _config;
        MyAPIs app1 = new MyAPIs();
        private readonly IJWTRepository _jWTManager;
        public NewUserOperationsController(IConfiguration config, IJWTRepository _jWTManager)
        {
            this._jWTManager = _jWTManager;
            _config = config;
        }


        [HttpPost("CreateUsers")]
        [Produces("application/json")]
        public UserResponse CreateUser([FromBody] Createuserpm param)
        {
            string MyBaseURL = _config["APIsCredentails:MyBaseURL"];
            string AuthPassword = _config["APIsCredentails:Authpassword"];
            string AuthUsername = _config["APIsCredentails:Authusername"];


            Random rd = new Random();
            int rand_num = rd.Next(1, 9000);
            CreateUserResponse crtuser = app1.CreateUser(MyBaseURL, rand_num, param.username, param.firstName, param.lastName, param.email, param.password, param.phone, 1, AuthUsername, AuthPassword);

            if (crtuser.code == 00)
            {
                return new UserResponse { ResponseCode = 00, ResponseDescription = "success", userid = rand_num };
            }
            else
            {
                return new UserResponse { ResponseCode = 11, ResponseDescription = "Unsuccesful" };
            }
        }




        //[HttpGet("GetUsers")]
        //[Produces("application/json")]
        //public GetUserResponse GetUsers(string username)
        //{
        //    string PetStoreBaseURL = _config["PetStore:PetStoreBaseURL"];


        //    AUTHReponse crtuer = app1.GetUser(PetStoreBaseURL, username);
        //    if (crtuer.firstName != null && crtuer.firstName != "")
        //    {
        //        return new GetUserResponse { ResponseCode = "00", ResponseDescription = "success", Userinfo = crtuer };
        //    }
        //    else
        //    {
        //        return new GetUserResponse { ResponseCode = "11", ResponseDescription = crtuer.message };
        //    }
        //}
    }
}
