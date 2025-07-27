using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingCodeFirst_FirstProject.Extensions
{
    internal static class DbSetExtension
    {
        public static void AddValues<T>(this DbSet<T> dbSet, IEnumerable<T> entities) where T : class, new()
        {
            if (dbSet == null)
            {
                throw new ArgumentNullException(nameof(dbSet));
            }

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (!entities.Any())
            {
                return; // No entities to add
            }

            foreach (var entity in entities)
            {
                dbSet.Add(entity);
            }
        }
    }
}
