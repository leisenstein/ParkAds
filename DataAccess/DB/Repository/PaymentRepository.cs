using DataAccess.DB.Contexts;
using DataAccess.DB.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DB.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private PaymentContext context;
        public PaymentRepository(PaymentContext context) : base(context)
        {
            this.context = context;
        }

        public override void Add(Payment entity)
        {
            context.Entry(entity.Booking.User).State = EntityState.Unchanged;
            context.Entry(entity.Booking.Spot).State = EntityState.Added;
            base.Add(entity);
        }

        public override Payment Get(int id)
        {
            return context.Payments
                .Include(p => p.Booking)
                .Include(p => p.Booking.Spot)
                .Include(p => p.Booking.User)
                .FirstOrDefaultAsync(p => p.Id == id).Result;
        }

        public PaymentContext PaymentContext
        {
            get { return Context as PaymentContext; }
        }
    }
}
