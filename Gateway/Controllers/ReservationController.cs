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
using Microsoft.Extensions.Logging;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private EnrichmentService enrichmentService = new EnrichmentService();
        private readonly ILogger<AdMicroService> _logger;
        public ReservationController(ILogger<AdMicroService> logger)
        {
            _logger = logger;
        }
        // POST: api/reservation
        [HttpPost]
        public ContentResult Post([FromBody] object reservationDTO)
        {
            object reservation = ReservationMapping.MapDTOToDomainObject(reservationDTO);
            _logger.LogInformation("Reservation created ," + DateTime.UtcNow);
            object returnedReservation = enrichmentService.Add(reservation);
            if (returnedReservation != null)
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(ReservationMapping.MapDomainToDTOObject(returnedReservation)),
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
            return ReservationMapping.MapDomainToDTOObject(enrichmentService.Get(id));
        }
    }
}