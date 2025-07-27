using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DemoForRepository.Models;

namespace DemoForRepository.DatabaseContext
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext()
        {
            // Set Initializer
            Database.SetInitializer(new ExampleDbInitializer());
            Configuration.LazyLoadingEnabled = true;

        }
        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }
    }
}
