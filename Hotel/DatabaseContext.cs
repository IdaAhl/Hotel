using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hotel
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Area> Area { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyHotelsDatabase.db");
        }
    }
}
