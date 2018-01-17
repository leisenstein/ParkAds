using Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DB.Contexts
{
    public class BookingContext : DbContext
    {
        public BookingContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source =LAPTOP-JHBMA0II; Initial Catalog = ParkAdsBookings; Integrated Security = True; Connect Timeout = 30;");
        }

        public virtual DbSet<Booking> Bookings { get; set; }
    }
}
