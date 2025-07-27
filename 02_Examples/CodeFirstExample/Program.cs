using System;
using System.Linq;

namespace CodeFirstExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DBContext.WorldCountries())
            {
                // Ensure the database is created always
                db.Database.Delete();
                db.Database.Create(); // This will create the database if it doesn't exist
                // db.Database.CreateIfNotExists(); // Use this if you want to create the database only if it doesn't exist

                // Create a new country
                Entities.Country country = new Entities.Country { Name = "United States" };
                db.Countries.Add(country);
                
                // Save changes to the database
                db.SaveChanges();
                
                // Retrieve and display the countries
                var countries = db.Countries.ToList();
                foreach (var c in countries)
                {
                    Console.WriteLine($"Country: {c.Name}");
                }
            }
        }
    }
}
