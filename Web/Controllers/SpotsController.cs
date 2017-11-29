using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using DataTransferObjects;
using Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace Web.Controllers
{
    [Route("spots")]
    public class SpotsController : Controller
    {
        private SessionController sessionController;
        [Route("")]
        public async Task<IActionResult> Spots()
        {
            sessionController = new SessionController(HttpContext);
            if (!sessionController.IsLoggedIn())
                return RedirectToAction("Index", "Home");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/spot");
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            SpotsViewModel viewModel = new SpotsViewModel();
            viewModel.spots = JsonConvert.DeserializeObject<IEnumerable<SpotDTO>>(response);
            viewModel.ad = await GetAdd();

            return View(viewModel);
        }

        private async Task<AdDTO> GetAdd()
        {
            AdController adController = new AdController();
            return await adController.Ad().ConfigureAwait(false);
        }
        
        [Route("reservation")]
        public async Task<IActionResult> Book()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:1914/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            ReservationDTO reservationDTO = new ReservationDTO();
            reservationDTO.DateTime = DateTime.Now;

            string json = JsonConvert.SerializeObject(reservationDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("api/reservation", content);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            return RedirectToAction("Spots", "Spots");
        }
    }
}