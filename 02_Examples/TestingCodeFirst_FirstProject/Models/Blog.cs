using System.Collections.Generic;

namespace TestingCodeFirst_FirstProject.Models
{
    internal class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
