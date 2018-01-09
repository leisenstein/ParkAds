using DataTransferObjects;
using System;
using System.ComponentModel;

namespace Web.Models
{
    public class PaymentViewModel
    {
        [DisplayName("Payment number")]
        public int Id { get; set; }

        [DisplayName("Created at")]
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public BookingDTO BookingDTO { get; set; }
    }
}
