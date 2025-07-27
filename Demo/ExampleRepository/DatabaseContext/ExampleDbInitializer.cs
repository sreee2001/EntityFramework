using System;
using System.Collections.Generic;
using System.Data.Entity;
using Repository.Utilities;

namespace ExampleRepository.DatabaseContext
{
    internal class ExampleDbInitializer : DropCreateDatabaseAlways<ExampleDbContext>
    {
        public override void InitializeDatabase(ExampleDbContext context)
        {
#if DEBUG
            Console.WriteLine("ExampleDbInitializer: InitializeDatabase called");
#endif
            base.InitializeDatabase(context);
        }

        protected override void Seed(ExampleDbContext context)
        {
#if DEBUG
            Console.WriteLine("ExampleDbInitializer: Seeding the Database");
#endif
            base.Seed(context);
            // Add additional Seeding of entries into Db in here

            context.Countries.ComboBoxDropDownItemAddValues(new List<string> { "Algeria", "Angola", "Argentina", "Australia", "Azerbaijan", "Bolivia", "Brazil", "Cameroon", "Canada", "Chad", "China", "Columbia", "CIS (Commonwealth of Independent States)", "Congo", "Cyprus", "Egypt", "Equitorial Guinea", "Faroes", "France", "Gabon", "Germany", "Ghana", "Guyana", "Hungary", "Indonesia", "Ireland", "Italy", "Kazakhstan", "Kurdistan", "Liberia", "Libya", "Malaysia", "Mauritania", "Mexico", "Mozambique", "Namibia", "Netherlands", "Niger", "Nigeria", "Norway", "Papua New Guinea", "Pakistan", "Peru", "Philippines", "Poland", "Qatar", "Romania", "Russia", "Sao Tome and Principe", "Senegal", "South Africa", "Tanzania", "Trinidad", "Turkey", "Turkmenistan", "United States", "United Kingdom", "Uruguay", "Venezuela", "Vietnam", "Yemen" });

            context.Continents.ComboBoxDropDownItemAddValues(new List<string> { "Africa", "Antarctica", "Asia", "Europe", "North America", "Oceania", "South America" });
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
