using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.ExternalServices
{
    public class AdService
    {
        private MemoryCache cache;

        public AdService()
        {
            cache = new MemoryCache(new MemoryCacheOptions());
        }
        public Ad Get()
        {
            if (cache.Count == 0)
            {
                Ad ads = GetFromSource().Result;
                while (ads.ImageData.Length < 100)
                    ads = GetFromSource().Result;
                cache.Set("ad", ads, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                Ad ad = (Ad)cache.Get("ad");
                return ad;
            }
            else
            {
                return (Ad)cache.Get("ad");
            }
        }
        public async Task<Ad> GetFromSource()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://dm.sof60.dk:81/");
            httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ad").ConfigureAwait(false);
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Ad>(jsonResponse);
        }
    }
}
