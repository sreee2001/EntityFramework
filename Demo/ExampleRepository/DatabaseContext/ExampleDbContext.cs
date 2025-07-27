using ExampleRepository.Models;
using System;
using System.Data.Entity;

namespace ExampleRepository.DatabaseContext
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext()
        {
            // Set Initializer
            Database.SetInitializer(new ExampleDbInitializer());
            Configuration.LazyLoadingEnabled = true;
#if DEBUG
            Console.WriteLine("ExampleDbContext: Database constructed");
#endif

        }
        // Define your DbSets here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }
    }
}
