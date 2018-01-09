using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SpotsViewModel
    {
        public IList<SpotDTO> Spots { get; set; }
        public AdDTO Ad { get; set; }
    }
}
