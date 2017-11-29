using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;
using DataTransferObjects;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        private HttpContext Context;
        public SessionController(HttpContext Context)
        {
            this.Context = Context;
        }

        public void CreateUserSession(object content)
        {
            Context.Session.SetString("User", Encode(content));
        }

        public bool IsLoggedIn()
        {
            return Context.Session.GetString("User") != null ? true : false;
        }

        public UserDTO GetUser()
        {
            string json = Decode(Context.Session.GetString("User"));
            return JsonConvert.DeserializeObject<UserDTO>(json);
        }
        private string Encode(object input)
        {
            var toBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input));
            return Convert.ToBase64String(toBytes);
        }

        private string Decode(object input)
        {
            var toBytes = Convert.FromBase64String(Context.Session.GetString("User"));
            return Encoding.UTF8.GetString(toBytes);
        }
    }
}