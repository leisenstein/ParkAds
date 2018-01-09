using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public Booking Booking { get; set; }
    }
}
