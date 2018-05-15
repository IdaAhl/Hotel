using Hotel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Area> Area { get; set; }
        public DbSet<Domain.Hotel> Hotel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyHotelsDatabase.db");
        }
    }
}
