using CodeFirstExample.Entities;
using System.Data.Entity;

namespace CodeFirstExample.DBContext
{
    internal class WorldCountries : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}
