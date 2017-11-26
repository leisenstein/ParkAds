using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Services
{
    public class ReservationEnrichmentService
    {
        private EmailMicroService emailMicroService = new EmailMicroService();
        public void CreateReservation()
        {
            emailMicroService.SendSimpleMessage();
        }
    }
}
