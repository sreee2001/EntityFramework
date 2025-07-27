using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoForRepository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is the entry point of the application.
            // You can add code here to initialize your application, 
            // set up dependency injection, or run any startup logic.
            Console.WriteLine("Welcome to the Demo For Repository!");

            try
            {
                // Example of using the ExampleDbContext
                using (var context = new DatabaseContext.ExampleDbContext())
                {
                    // Ensure the database is created
                    context.Database.CreateIfNotExists();
                    Console.WriteLine("Database is ready to use.");

                    // You can add your logic here to interact with the database
                    // For example, adding entities, querying, etc.
                    // For now, some DropDownItems are seeded in the ExampleDbInitializer.

                    // read the seeded data
                    var countries = context.Countries.ToList();
                    foreach (var country in countries)
                    {
                        Console.WriteLine($"Country: {country.Name}");
                    }

                    Console.WriteLine("Database is ready to use.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during database operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            // finally wait for the user to press a key before exiting
            Console.WriteLine("Demo Finished!");
        }
    }
}
