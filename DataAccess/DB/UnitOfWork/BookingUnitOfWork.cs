using DataAccess.DB.Contexts;
using DataAccess.DB.Interfaces;
using DataAccess.DB.Repository;

namespace DataAccess.DB.UnitOfWork
{
    public class BookingUnitOfWork : IUnitOfWork<IBookingRepository>
    {
        private readonly BookingContext context;

        public BookingUnitOfWork(BookingContext context)
        {
            this.context = context;
            Repository = new BookingRepository(this.context);
        }

        public IBookingRepository Repository { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
