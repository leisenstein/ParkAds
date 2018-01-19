using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using MicroServices.FactoryReservation;

namespace MicroServices.Services
{
    public class EnrichmentService
    {
        private EmailMicroService emailMicroService = new EmailMicroService();
        private ReservationEnrichmentService reservationEnrichmentService = new ReservationEnrichmentService();
        private AdMicroService adMicroService = new AdMicroService();

        public bool Add(object reservation)
        {
            if (reservationEnrichmentService.Add(reservation) == true)
            {
                     Ad ad = adMicroService.Get();
                    emailMicroService.SendSimpleMessage(ad, reservation);
                return true;
            }
            return false;
        }
        public object Get(string id)
        {
            return ReservationFactory.Get(id);
        }

    }
}
