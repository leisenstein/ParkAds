using DataAccess.DB.Contexts;
using DataAccess.DB.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DB.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private BookingContext context;
        public BookingRepository(BookingContext context) : base(context)
        {
            this.context = context;
        }

        public override void Add(Booking entity)
        {
            context.Entry(entity.User).State = EntityState.Unchanged;
            context.Entry(entity.Spot).State = EntityState.Unchanged;
            base.Add(entity);
        } 

        public override Booking Get(int id)
        {
            return context.Bookings
                .Include(b => b.Spot)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id).Result;
        }

        public override void Update(Booking entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            base.Update(entity);
        }
        public BookingContext BookingContext
        {
            get { return Context as BookingContext; }
        }
    }
}
