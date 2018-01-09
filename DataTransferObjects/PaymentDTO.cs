using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public BookingDTO BookingDTO { get; set; }
    }
}
