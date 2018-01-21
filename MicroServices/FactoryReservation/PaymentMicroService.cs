using Domain;
using DataAccess.DB.UnitOfWork;
using DataAccess.DB.Contexts;

namespace MicroServices.FactoryReservation
{
    public sealed class PaymentMicroService
    {
        private PaymentUnitOfWork paymentUnitOfWork = new PaymentUnitOfWork(new PaymentContext());
        public Payment Add(Payment payment)
        {
            payment.Booking.IsPayed = true;
            paymentUnitOfWork.Repository.Add(payment);
            return paymentUnitOfWork.Complete() > 0 ? payment : null;
        }

        public object Get(int id)
        {
            return paymentUnitOfWork.Repository.Get(id);
        }
    }
}
