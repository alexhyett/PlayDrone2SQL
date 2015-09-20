
namespace PlayDrone2SQL
{
    using Models;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryCache : IWriter<Category>, IReader
    {
        private Dictionary<string, Guid> categoryLookup = new Dictionary<string, Guid>();

        private IWriter<Category> writer;

        private IReader reader;
        public CategoryCache(IWriter<Category> writer, IReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        public int Count()
        {
            if(categoryLookup.Any())
            {
                return categoryLookup.Count();
            }

            return reader.Count();
        }

        public bool Exists(string name)
        {
            var exists = categoryLookup.ContainsKey(name);
            if (!exists)
            {
                exists = reader.Exists(name);
            }

            return exists;
        }

        public Guid GetId(string name)
        {
           var exists = categoryLookup.ContainsKey(name);

            if(!exists)
            {
                return reader.GetId(name);
            }

            return categoryLookup[name];
        }

        public void Save(Category category)
        {
            if(!categoryLookup.ContainsKey(category.Name))
            {
                categoryLookup.Add(category.Name, category.Id);
            }

            writer.Save(category);
        }

        public void SaveMany(List<Category> categories)
        {
            foreach(var category in categories)
            {
                if (!categoryLookup.ContainsKey(category.Name))
                {
                    categoryLookup.Add(category.Name, category.Id);
                }
            }

            writer.SaveMany(categories);
        }
    }
}
