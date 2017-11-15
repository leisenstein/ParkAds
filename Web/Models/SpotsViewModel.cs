using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SpotsViewModel
    {
        public IEnumerable<SpotDTO> spots { get; set; }
    }
}
