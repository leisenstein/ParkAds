using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using DataTransferObjects;
using Newtonsoft.Json;
using System.Threading;

namespace Web.Controllers
{
    public class AdController : Controller
    {
        public async Task<AdDTO> Ad()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5011/");
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ad");

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            AdDTO adDTO = JsonConvert.DeserializeObject<AdDTO>(response);

            return adDTO;
        }
    }
}