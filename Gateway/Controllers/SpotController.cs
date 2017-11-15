using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataTransferObjects;
using Gateway.Mapping;
using MicroServices.Services;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/spot")]
    public class SpotController : Controller
    {
        private SpotMicroService spotMicroService = new SpotMicroService();

        [HttpGet]
        public IEnumerable<SpotDTO> Get()
        {
            return SpotMapping.MapDomainToDTOCollection(spotMicroService.GetAll());
        }
    }
}