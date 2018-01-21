using System;

namespace MessageReceiver
{
    public class Receive
    {
        public static void Main(string[] args)
        {
            BookingReceive reservationReceive = new BookingReceive();
            PaymentReceive paymentReceive = new PaymentReceive();
        }
    }
}
