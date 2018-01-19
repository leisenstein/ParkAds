using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DataTransferObjects;
using Web.Mapping;

namespace Web.Controllers
{
    [Route("signin")]
    public class SignInController : Controller
    {
        private SessionController sessionController;

        [Route("")]
        [HttpGet]
        public ActionResult SignIn()
        {
            sessionController = new SessionController(HttpContext);

            if (sessionController.IsLoggedIn())
                return RedirectToAction("Spots", "Spots");

            return View();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> SignIn(SignInViewModel signin)
        {
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:5011/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                UserDTO userDTO = UserMapping<SignInViewModel>.MapViewModelDTOObject(signin);
                string json = JsonConvert.SerializeObject(userDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("api/user", content);

                int statusCode = (int)httpResponseMessage.StatusCode;
                switch (statusCode)
                {
                    case 412:
                        return RedirectToAction("SignIn", "SignIn", new { responseMessage = "An user with this email has alredy exists!" });
                    case 200:
                        string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                        UserDTO signedUser = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);

                        sessionController = new SessionController(HttpContext);
                        sessionController.CreateUserSession(signedUser);

                        return RedirectToAction("Spots", "Spots");
                    default:
                        return RedirectToAction("SignIn", "SignIn", new { responseMessage = "" });
                }
            }

            return View(signin);
        }
    }
}