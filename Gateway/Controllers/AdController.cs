using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Services;
using DataTransferObjects;
using Gateway.Mapping;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/ad")]
    public class AdController : Controller
    {
        private AdMicroService adMicroService = new AdMicroService();

        [HttpGet]
        public AdDTO Get()
        {
            return AdMapping.MapDomainToDTOObject(adMicroService.Get());
        }
    }
}