using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingCodeFirst_FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(var db = new DBContext.BloggingContext())
            {
                // Ensure the database is created
                db.Database.CreateIfNotExists();

                {
                    // Create a new blog
                    var blog = new Models.Blog { Name = "My First Blog" };
                    db.Blogs.Add(blog);

                    // Save changes to the database
                    db.SaveChanges();
                }

                {
                    // Create a new post
                    var post = new Models.Post { Title = "Hello World", Content = "This is my first post!", BlogId = blog.BlogId };
                    db.Posts.Add(post);
                    // Save changes to the database

                    db.SaveChanges();
                }

                {
                    // Retrieve and display the blog and its posts
                    //var blogs = db.Blogs.Include(b => b.Posts).ToList();
                    var blogs = db.Blogs.Include("Posts").ToList(); // Fixed: Use string-based Include for EF6

                    foreach (var b in blogs)
                    {
                        Console.WriteLine($"Blog: {b.Name}");
                        foreach (var p in b.Posts)
                        {
                            Console.WriteLine($" - Post: {p.Title}");
                        }
                    }
                }
            }
        }
    }
}
