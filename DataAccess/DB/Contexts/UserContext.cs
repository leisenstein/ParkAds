using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DB.Contexts
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source =TEDO\SQLEXPRESS; Initial Catalog = ParkAdsUsers; Integrated Security = True; Connect Timeout = 30;");
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
