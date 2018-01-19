using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Services;
using DataTransferObjects;
using Gateway.Mapping;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/ad")]
    public class AdController : Controller
    {
        private AdMicroService adMicroService = new AdMicroService();
        private IMemoryCache cache;
        private readonly ILogger<AdMicroService> _logger;
        public AdController(IMemoryCache cache, ILogger<AdMicroService> logger)
        {
            this.cache = cache;
            _logger = logger;
        }
        [HttpGet]
        public AdDTO Get()
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            string key = "ad";
            AdDTO ad ;
            if (!cache.TryGetValue<AdDTO>(key, out ad))
            {
                do
                    ad = AdMapping.MapDomainToDTOObject(adMicroService.Get());
                while (ad.AdEncoding.Length < 100);
                cache.Set<AdDTO>(key, ad, cacheExpirationOptions);
            }
            _logger.LogInformation("Ad request ,"+DateTime.UtcNow);
            return (AdDTO)cache.Get("ad");
        }
    }
}