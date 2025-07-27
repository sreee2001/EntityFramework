using CodeFirstExample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstExample.DBContext
{
    internal class WorldCountries : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}
