using System;
using Domain;
using MicroServices.FactoryReservation;

namespace MicroServices.Services
{
    public class ReservationMicroService
    {
        public object Add(object reservation)
        {
            return ReservationFactory.Add(reservation);
        }

        public object Get(string id)
        {
            return ReservationFactory.Get(id);
        }
    }
}
