using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Services;
using Newtonsoft.Json;
using DataTransferObjects;
using Domain;
using Gateway.Mapping;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private EnrichmentService reservationEnrichmentService = new EnrichmentService();
        // POST: api/reservation
        [HttpPost]
        public ContentResult Post([FromBody] object reservationDTO)
        {
            object reservation = ReservationMapping.MapDTOToDomainObject(reservationDTO);
            if (reservationEnrichmentService.Add(reservation))
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(ReservationMapping.MapDomainToDTOObject(reservation)),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            else
                return new ContentResult()
                {
                    StatusCode = 412
                };
        }

        [HttpGet]
        [Route("id/{id}")]
        public object Get(string id)
        {
            return ReservationMapping.MapDomainToDTOObject(reservationEnrichmentService.Get(id));
        }
    }
}