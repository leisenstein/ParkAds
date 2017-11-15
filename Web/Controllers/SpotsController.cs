using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using DataTransferObjects;
using Web.Models;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class SpotsController : Controller
    {
        [Route("spots")]
        public async Task<IActionResult> Spots()
        {
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
    }
}