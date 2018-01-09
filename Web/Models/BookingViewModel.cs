using DataTransferObjects;
using System;
using System.ComponentModel;

namespace Web.Models
{
    public class BookingViewModel
    {
        [DisplayName("Booking number")]
        public int Id { get; set; }

        [DisplayName("Created at")]
        public DateTime DateTime { get; set; }

        [DisplayName("Payment done")]
        public bool IsPayed { get; set; }
        public UserDTO UserDTO { get; set; }
        public SpotDTO SpotDTO { get; set; }
    }
}
