﻿namespace NewAPIConsume.RequestObject
{
    public class CreateUserResponse
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public int userstatus { get; set; }
        public int code { get; set; }
        public string message { get; set; }

    }
}
