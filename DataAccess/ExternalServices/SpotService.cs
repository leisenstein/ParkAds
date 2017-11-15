using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ExternalServices
{
    public class SpotService
    {
        public async Task<IEnumerable<Spot>> GetAll()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ucn-parking.herokuapp.com/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("CONSUME SERVICE");
            stringBuilder.AppendLine("Time: " + DateTime.Now);
            // File.AppendAllText(@"c:\Users\Georgi Karavasilev\Desktop\log.txt", stringBuilder.ToString() + Environment.NewLine);
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("places.json").ConfigureAwait(false);
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<IEnumerable<Spot>>(jsonResponse);
        }
    }
}
