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
        public WorldCountries()
            //: base("name=dev")
            //: base("name=qa")
            //: base("name=prod")            
        {
            Database.SetInitializer(new DBInitializer());
        }
        public DbSet<Country> Countries { get; set; }
    }

    internal class DBInitializer : DropCreateDatabaseAlways<WorldCountries>
    {
        protected override void Seed(WorldCountries context)
        {
            context.Countries.Add(new Country { Name = "United States" });
            context.Countries.Add(new Country { Name = "Canada" });
            context.Countries.Add(new Country { Name = "Mexico" });
            base.Seed(context);
        }
    }
}
