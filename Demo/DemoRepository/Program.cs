using DemoRepository.DatabaseContext;
using DemoRepository.Models;
using System;
using System.Linq;

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

            DemoUsingContextDirectly();

            // finally wait for the user to press a key before exiting
            Console.WriteLine("Demo Finished!");
        }

        private static void DemoUsingContextDirectly()
        {
            // This method is a placeholder for testing purposes.
            // You can implement logic here to test the ExampleDbContext directly.
            // For example, you can create an instance of ExampleDbContext and perform operations on it.
            Console.WriteLine("Testing ExampleDbContext directly...");
            try
            {
                // Example of using the ExampleDbContext
                using (var context = new ExampleDbContext())
                {
                    // Ensure the database is created
                    context.Database.CreateIfNotExists();
                    Console.WriteLine("Database is ready to use.");

                    // You can add your logic here to interact with the database
                    // For example, adding entities, querying, etc.
                    // For now, some DropDownItems are seeded in the ExampleDbInitializer.

                    // read the seeded data
                    {
                        Console.WriteLine("Reading seeded data...");
                        //var countries = context.Countries.ToList();
                        //foreach (var country in countries)
                        //{
                        //    Console.WriteLine($"Country: {country.Name}");
                        //}
                        var continents = context.Continents.ToList();
                        foreach (var continent in continents)
                        {
                            Console.WriteLine($"Country: {continent.Name}");
                        }
                        Console.WriteLine();
                    }

                    var newContinentName = "***New Continent***";

                    // Add etries to the Continents table
                    {
                        Console.WriteLine("Adding a new continent...");
                        Console.WriteLine();
                        context.Continents.Add(new Continent { Name = newContinentName });

                        context.SaveChanges();
                    }

                    // read the updated continent data
                    {
                        var continents = context.Continents.ToList();
                        foreach (var continent in continents)
                        {
                            Console.WriteLine($"Country: {continent.Name}");
                        }
                        Console.WriteLine();
                    }

                    // delete the added continent
                    {
                        Console.WriteLine("Deleting the added continent...");
                        var continentToDelete = context.Continents.FirstOrDefault(c => c.Name == newContinentName);
                        if (continentToDelete != null)
                        {
                            context.Continents.Remove(continentToDelete);
                            context.SaveChanges();
                        }
                        Console.WriteLine();
                    }

                    // read the updated continent data
                    {
                        var continents = context.Continents.ToList();
                        foreach (var continent in continents)
                        {
                            Console.WriteLine($"Country: {continent.Name}");
                        }
                        Console.WriteLine();
                    }


                    Console.WriteLine("Database is ready to use.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during database operations
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}
