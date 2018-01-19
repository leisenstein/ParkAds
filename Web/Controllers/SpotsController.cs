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
            httpClient.BaseAddress = new Uri("http://localhost:5011/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/spot");
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            SpotsViewModel viewModel = new SpotsViewModel();
            viewModel.Spots = JsonConvert.DeserializeObject<IList<SpotDTO>>(response);
            viewModel.Ad = await GetAdd();

            return View(viewModel);
        }

        private async Task<AdDTO> GetAdd()
        {
            AdController adController = new AdController();
            return await adController.Ad().ConfigureAwait(false);
        }
    }
}