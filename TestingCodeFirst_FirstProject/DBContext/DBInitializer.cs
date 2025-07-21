using System.Collections.Generic;
using System.Data.Entity;
using TestingCodeFirst_FirstProject.Models;

namespace TestingCodeFirst_FirstProject.DBContext
{
    internal class DBInitializer : DropCreateDatabaseAlways<BloggingContext> 
    {
        public override void InitializeDatabase(BloggingContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(BloggingContext context)
        {
            base.Seed(context);
            // Seed initial data if needed
            // manual insertion
            {
                for(int i = 1; i <= 10; i++)
                {
                    context.Blogs.Add(new Blog { Name = $"Initial Blog {i}" });
                    for(int j = 1; j <= 5; j++)
                    {
                        context.Posts.Add(new Post { Title = $"Post {j} for Blog {i}", Content = $"Content for post {j} in blog {i}", BlogId = i });
                    }
                }
            }

            // automatic insertion using list
            {
                context.Blogs.AddRange(new List<Blog>
                {
                    new Blog { Name = "Blog A" },
                    new Blog { Name = "Blog B" },
                    new Blog { Name = "Blog C" }
                });
                context.Posts.AddRange(new List<Post>
                {
                    new Post { Title = "Post 1 for Blog A", Content = "Content for post 1 in blog A", BlogId = 1 },
                    new Post { Title = "Post 2 for Blog A", Content = "Content for post 2 in blog A", BlogId = 1 },
                    new Post { Title = "Post 1 for Blog B", Content = "Content for post 1 in blog B", BlogId = 2 },
                    new Post { Title = "Post 1 for Blog C", Content = "Content for post 1 in blog C", BlogId = 3 }
                });
            }
        }

        // these can be implemented by overriding if you want a different functionality
        //public override string ToString()
        //{
        //    return base.ToString();
        //}

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}
