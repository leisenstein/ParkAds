using Domain;
using MessageSender;
using MicroServices.FactoryReservation;

namespace MicroServices.Services
{
    public class EnrichmentService
    {
        public object Add(object reservation)
        {
            object returnedReservation = ReservationAdded(reservation);
            if (returnedReservation != null)
            {
                SendMail(GetAd(), returnedReservation);
                return returnedReservation;
            }

            return null;
        }
        public object Get(string id)
        {
            return ReservationFactory.Get(id);
        }

        private object ReservationAdded(object reservation)
        {
            return ReservationFactory.Add(reservation);
        }

        private Ad GetAd()
        {
            AdSend adSend = new AdSend();
            Ad ad = adSend.AdRequest();
            adSend.CloseConnection();

            return ad;
        }

        private void SendMail(Ad ad, object reservation)
        {
            EmailSend emailSend = new EmailSend();
            emailSend.SendEmail(ad, reservation);
            emailSend.CloseConnection();
        } 
    }
}
