using Microsoft.AspNetCore.Mvc;
using NewAPIConsume.RequestObject;
using NewAPIConsume.ResponseObject;
using Newtonsoft.Json;
using RestSharp;

namespace NewAPIConsume.ExternalAPIs
{
    public class MyAPIs
    {




        public TokenResponse1 Login(string MyBaseURL, string username, string password)
        {
            try
            {
                string url = MyBaseURL + "/api/UserOperations/Login";
                Loginpm reqpram = new Loginpm()
                {
                    username = username,
                    password = password
                };

                var client = new RestSharp.RestClient(url);
                var request = new RestSharp.RestRequest(url,RestSharp.Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddJsonBody(reqpram);

                RestResponse response = client.Execute(request);


                if (response.StatusCode.ToString() == "NotFound" || response.StatusCode.ToString() == "ServiceUnavailable" || response.StatusCode.ToString() == "Unauthorized" || response.StatusCode == 0)
                {
                    TokenResponse1 respnerr = new TokenResponse1();
                    return null;
                }

                TokenResponse1 returnData = JsonConvert.DeserializeObject<TokenResponse1>(response.Content);
                return returnData;
            }
            catch (Exception ex)
            {
                TokenResponse1 respexc = new TokenResponse1();
                respexc.Token = ex.Message;
                return respexc;
            }
        }




        [HttpPost("CreateUser")]
        [Produces("application/json")]
        public CreateUserResponse CreateUser(string MyBaseURL, int id, string username, string firstName, string lastName, string email, string password, string phone, int userstatus, string AuthUsername, string AuthPassword)
        {
            try
            {
                
                string url = MyBaseURL + "/api/UserOperations/CreateUser";
                string token = Login(MyBaseURL, AuthUsername, AuthPassword).Token;
                CreateUserParam param = new CreateUserParam
                {
                    username = username,
                    firstName = firstName,
                    lastName = lastName,
                    password = password,
                    phone = phone,
                };
                var client = new RestSharp.RestClient(url);
                var request = new RestSharp.RestRequest(url, Method.Post);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "bearer " + token);
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddJsonBody(param);
                RestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "NotFound" || response.StatusCode.ToString() == "ServiceUnavailable" || response.StatusCode.ToString() == "Unauthorized" || response.StatusCode == 0)
                {
                    CreateUserResponse respnerr = new CreateUserResponse();
                    respnerr.code = 11;
                    respnerr.message = "Unsuccesful";
                    return respnerr;
                }




                CreateUserResponse returnData = JsonConvert.DeserializeObject<CreateUserResponse>(response.Content);
                return returnData;
            }
            catch (Exception ex)
            {
                CreateUserResponse respexc = new CreateUserResponse();
                respexc.code = 00;
                respexc.message = ex.Message;
                return respexc;
            }
        }

    }




   







}
        
