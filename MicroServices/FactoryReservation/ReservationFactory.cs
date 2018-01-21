using Domain;
using MessageSender;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MicroServices.FactoryReservation
{
    public  class ReservationFactory
    {
        public static object Add(object reservation)
        {
            if(reservation.GetType().ToString().Equals("Domain.Payment"))
            {
                PaymentSend paymentSend = new PaymentSend();
                string paymentResponse = paymentSend.SendMessage(JsonConvert.SerializeObject(reservation));
                paymentSend.CloseConnection();

                return JsonConvert.DeserializeObject<Payment>(paymentResponse);

                //PaymentMicroService paymentMicroService = new PaymentMicroService();

                //Payment payment = (Payment)reservation;

                //if (paymentMicroService.Add(payment))
                //    if (PayBooking(payment.Booking))
                //        return true;
            }
            else
            {
                BookingSend bookingSend = new BookingSend();
                string bookingResponse = bookingSend.SendMessage(JsonConvert.SerializeObject(reservation));
                bookingSend.CloseConnection();

                return JsonConvert.DeserializeObject<Booking>(bookingResponse);
            }
        }

        public static object Get(string reservationId)
        {
            int id = ExtractNumericPartOfId(reservationId);
            object IReservation = null;

            if (reservationId.Contains("booking"))
                IReservation = new BookingMicroService();
            else
                IReservation = new PaymentMicroService();

            return IReservation;//IReservation.Get(id);
        }

        private static bool PayBooking(Booking booking)
        {
            BookingMicroService bookingMicroService = new BookingMicroService();
            return bookingMicroService.PayBooking(booking);
        }

        private static int ExtractNumericPartOfId(string reservationId)
        {
            Regex regex = new Regex(@"([0-9]{1,})", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(reservationId);
            return Int32.Parse(matches[0].Value);
        }
    }
}
