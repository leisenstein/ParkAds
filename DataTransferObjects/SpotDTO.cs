using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class SpotDTO
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int FreeSpots { get; set; }
    }
}
