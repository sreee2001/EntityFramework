using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingCodeFirst_FirstProject.Models;

namespace TestingCodeFirst_FirstProject.DBContext
{
    internal class BloggingContext : DbContext
    {
        public BloggingContext()
            //: base("name=dev")
            //: base("name=qa")
            //: base("name=prod")            
        { 
            Database.SetInitializer(new DBInitializer());
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
