using System;
using Domain;
using MicroServices.FactoryReservation;

namespace MicroServices.Services
{
    public class ReservationEnrichmentService
    {
        private EmailMicroService emailMicroService = new EmailMicroService();
        public bool Add(object reservation)
        {
            return ReservationFactory.Add(reservation);
            //emailMicroService.SendSimpleMessage();
        }

        public object Get(string id)
        {
            return ReservationFactory.Get(id);
        }
    }
}
