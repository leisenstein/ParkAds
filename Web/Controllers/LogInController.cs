using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using DataTransferObjects;
using Newtonsoft.Json;
using Web.Mapping;
using System.Text;

namespace Web.Controllers
{
    [Route("login")]
    public class LogInController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel login)
        {
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://localhost:1914/");
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/user/email/" + login.Email);

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(response);

                if (userDTO.Password.Equals(login.Password))
                    return RedirectToAction("Spots", "Spots");
            }
            return View();
        }
    }
}