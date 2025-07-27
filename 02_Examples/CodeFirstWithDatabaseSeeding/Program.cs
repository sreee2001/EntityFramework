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
