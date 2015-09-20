
namespace PlayDrone2SQL
{
    using System.Collections.Generic;
    using Models;
    using System;

    public class CategoryLogger : IWriter<Category>, IReader
    {
        private IWriter<Category> writer;

        private IReader reader;

        private ILogger log;
        public CategoryLogger(IWriter<Category> writer, IReader reader, ILogger log)
        {
            this.writer = writer;
            this.log = log;
            this.reader = reader;
        }

        public void Save(Category category)
        {
            log.LogOperation(string.Format("Starting save of category: {0}.", category.Name));
            writer.Save(category);
            log.LogOperation(string.Format("Finished save of category: {0}.", category.Name));
        }

        public void SaveMany(List<Category> categories)
        {
            categories.ForEach(i => log.LogOperation(string.Format("Starting save of category: {0}.", i.Name)));
            writer.SaveMany(categories);
            categories.ForEach(i => log.LogOperation(string.Format("Finished save of category: {0}.", i.Name)));
        }

        public int Count()
        {
            return reader.Count();
        }

        public Guid GetId(string name)
        {
            return reader.GetId(name);
        }

        public bool Exists(string name)
        {
            return reader.Exists(name);
        }
    }
}
