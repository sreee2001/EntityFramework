using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utilities
{
    public static class DbSetExtension
    {
        /// <summary>
        /// Adds ComboboxDropDownItems into a DbSet using a list of strings
        /// </summary>
        /// <param name="set"></param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        public static void ComboBoxDropDownItemAddValues<T>(this DbSet<T> set, List<string> values) where T : ComboBoxDropDownItem, new()
        {
            set.Add(new T { Id = 1, Name = null, SortOrder = 1 });
            int i = 2;
            foreach (string value in values)
            {
                set.Add(new T { Id = i, Name = value, SortOrder = i });
                i++;
            }
        }
    }
}
