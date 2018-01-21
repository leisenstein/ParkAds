using System;

namespace MessageReceiver
{
    public class Receive
    {
        public static void Main(string[] args)
        {
            BookingReceive bookingReceive = new BookingReceive();
            PaymentReceive paymentReceive = new PaymentReceive();
            EmailReceive emailReceive = new EmailReceive();
            AdReceive adReceive = new AdReceive();
        }
    }
}
