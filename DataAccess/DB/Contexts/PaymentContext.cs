using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DB.Contexts
{
    public class PaymentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source =.\GKARAVASILEV; Initial Catalog = ParkAdsPayments; Integrated Security = True; Connect Timeout = 30;");
        }

        public virtual DbSet<Payment> Payments { get; set; }
    }
}
