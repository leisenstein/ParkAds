using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPayed { get; set; }
        public UserDTO UserDTO { get; set; }
        public SpotDTO SpotDTO { get; set; }
    }
}
