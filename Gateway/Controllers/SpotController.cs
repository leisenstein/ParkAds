using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataTransferObjects;
using Gateway.Mapping;
using MicroServices.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/spot")]
    public class SpotController : Controller
    {
        private SpotMicroService spotMicroService = new SpotMicroService();
        private IMemoryCache cache;
        private readonly ILogger<AdMicroService> _logger;
        public SpotController(IMemoryCache cache, ILogger<AdMicroService> logger)
        {
            this.cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<SpotDTO>> GetAsync()
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SetAbsoluteExpiration(TimeSpan.FromMilliseconds(5000));
            List<string> list = new List<string> { "Budolfi Plads", "C W Obel", "Friis", "Føtex", "Gåsepigen", "Kennedy Arkaden", "Kongrescenter", "Musikkens Hus", "Palads", "Salling", "Sauers Plads", "Sømandshjemmet","Toldbodgade", "Østre Havn" };
            string key0 = "Budolfi Plads";
            IEnumerable<SpotDTO> spots = new List<SpotDTO>();
            List<SpotDTO> listspots = new List<SpotDTO>();
            SpotDTO spot;
            if (!cache.TryGetValue(key0, out spot))
            {
                spots = SpotMapping.MapDomainToDTOCollection(await spotMicroService.GetAllAsync());
                spots.ToList().ForEach(i => cache.Set<SpotDTO>(i.Name, i, cacheExpirationOptions));
            }
            list.ForEach(i => listspots.Add((SpotDTO)cache.Get(i)));
            _logger.LogInformation("Spots request ," + DateTime.UtcNow);
            return listspots;
            //IEnumerable<SpotDTO> spotss = SpotMapping.MapDomainToDTOCollection(await spotMicroService.GetAllAsync());
            //return spotss;
        }
    }
}