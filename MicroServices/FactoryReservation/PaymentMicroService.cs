using Domain;
using DataAccess.DB.UnitOfWork;
using DataAccess.DB.Contexts;

namespace MicroServices.FactoryReservation
{
    public sealed class PaymentMicroService : IReservation
    {
        private PaymentUnitOfWork paymentUnitOfWork = new PaymentUnitOfWork(new PaymentContext());
        public bool Add(object reservation)
        {
            Payment payment = (Payment)reservation;
            payment.Booking.IsPayed = true;
            paymentUnitOfWork.Repository.Add(payment);
            return paymentUnitOfWork.Complete() > 0;
        }

        public object Get(int id)
        {
            return paymentUnitOfWork.Repository.Get(id);
        }
    }
}
