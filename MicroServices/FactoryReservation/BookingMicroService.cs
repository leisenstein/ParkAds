using DataAccess.DB.Contexts;
using DataAccess.DB.UnitOfWork;
using Domain;
using MicroServices.FactoryReservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.FactoryReservation
{
    public sealed class BookingMicroService
    {
        private BookingUnitOfWork bookingUnitOfWork = new BookingUnitOfWork(new BookingContext());
        public Booking Add(Booking booking)
        {
            bookingUnitOfWork.Repository.Add(booking);
            return bookingUnitOfWork.Complete() > 0 ? booking : null;
        }

        public object Get(int id)
        {
            return bookingUnitOfWork.Repository.Get(id);
        }

        public bool PayBooking(object reservation)
        {
            Booking booking = Cast(reservation);
            booking.IsPayed = true;
            bookingUnitOfWork.Repository.Update(booking);
            return bookingUnitOfWork.Complete() > 0;
        }

        private Booking Cast(object reservation)
        {
            return (Booking)reservation;
        }
    }
}
