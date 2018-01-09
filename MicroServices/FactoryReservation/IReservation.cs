using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.FactoryReservation
{
    public interface IReservation
    {
        bool Add(object reservation);

        object Get(int id);
    }
}
