using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPayed { get; set; }
        public User User { get; set; }
        public Spot Spot { get; set; }
    }
}
