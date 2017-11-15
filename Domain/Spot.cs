using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Spot
    {
        public string name { get; set; }
        public string is_open { get; set; }
        public string is_payment_active { get; set; }
        public string status_park_place { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int max_count { get; set; }
        public int free_count { get; set; }
    }
}
