using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicroServices.Services;
using Newtonsoft.Json;
using DataTransferObjects;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private ReservationEnrichmentService reservationEnrichmentService = new ReservationEnrichmentService();
        // POST: api/reservation
        [HttpPost]
        public ContentResult Post([FromBody] ReservationDTO reservationDTO)
        {
            //User user = UserMapping.MapDTOToDomainObject(reservationDTO);
            reservationEnrichmentService.CreateReservation();
            return new ContentResult()
            {
                Content = "reservation",
                ContentType = "text/plain",
                StatusCode = 200
            };
            //else
            //    return new ContentResult()
            //    {
            //        StatusCode = 412
            //    };
        }
    }
}