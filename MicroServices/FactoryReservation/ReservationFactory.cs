using Domain;
using System;
using System.Text.RegularExpressions;

namespace MicroServices.FactoryReservation
{
    public  class ReservationFactory
    {
        public static bool Add(object reservation)
        {
            IReservation IReservation = null;
            var asd = reservation.GetType().ToString();
            if(reservation.GetType().ToString().Equals("Domain.Payment"))
            {
                IReservation = new PaymentMicroService();

                Payment payment = (Payment)reservation;

                if (IReservation.Add(payment))
                    if (PayBooking(payment.Booking))
                        return true;

                return false;
            }
            else
            {
                IReservation = new BookingMicroService();

                Booking booking = (Booking)reservation;

                return IReservation.Add(booking);
            }
        }

        public static object Get(string reservationId)
        {
            int id = ExtractNumericPartOfId(reservationId);
            IReservation IReservation = null;

            if (reservationId.Contains("booking"))
                IReservation = new BookingMicroService();
            else
                IReservation = new PaymentMicroService();

            return IReservation.Get(id);
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
