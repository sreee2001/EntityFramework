using ExampleRepository.DatabaseContext;
using ExampleRepository.Models;
using Repository.Interfaces;
using System;
using System.Data.Entity;
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

            int choice = 0;

            while (choice != 3)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Demo using ExampleDbContext directly");
                Console.WriteLine("2. Demo using DbService");
                Console.WriteLine("3. Exit");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Welcome to the First Demo For Repository!");
                            DemoUsingContextDirectly();
                            Console.WriteLine("Demo Finished!");
                            Console.WriteLine();
                            break;
                        case 2:
                            Console.WriteLine("Second Demo for the Repository using DbService");
                            DemoUsingDbService();
                            Console.WriteLine("Second Demo Finished!");
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine("Exiting the application.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                }
            }
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

        private static void DemoUsingDbService()
        {
            // This method is a placeholder for testing purposes.
            // You can implement logic here to test the ExampleDbContext directly.
            // For example, you can create an instance of DbService and perform operations on it.
            Console.WriteLine("Testing DbService ...");
            try
            {
                {
                    // Get or Create an instance of the ExampleDbService
                    IDbService dbService = new ExampleDbService();

                    // as lazy Loading is enabled, please reset the context
                    dbService.ResetContext();

                    //dbService.Set<Continent>().Add(new Continent { Name = "New Continent" });
                    DbSet<Continent> continents = dbService.Set<Continent>();

                    Console.WriteLine("List of continents from Database Seed");
                    foreach (var continent in continents)
                    {
                        Console.WriteLine($"Continent: {continent.Name}");
                    }

                    var newContinentName = "*** New Continent ***";
                    // Add a new continent
                    continents.Add(new Continent() { Name = newContinentName });

                    dbService.SaveChanges();
                }

                {
                    IDbService newDbServiceInstace = new ExampleDbService();

                    newDbServiceInstace.ResetContext();

                    DbSet<Continent> continentsListRenewed = newDbServiceInstace.Set<Continent>();

                    Console.WriteLine("Updated List of continents from Database using a fresh DbService Instance");
                    foreach (var continent in continentsListRenewed)
                    {
                        Console.WriteLine($"Continent: {continent.Name}");
                    }
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
