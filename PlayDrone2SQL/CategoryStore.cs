
namespace PlayDrone2SQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Repository;

    /// <summary>
    /// The SQL category store.
    /// </summary>
    public class CategoryStore : IWriter<Models.Category>, IReader
    {
        /// <summary>
        /// Count the categories on the database.
        /// </summary>
        /// <returns>
        /// The number of categories.
        /// </returns>
        public int Count()
        {
            using (var db = new MarketDbContainer())
            {
                return db.Categories.Count();
            }
        }


        /// <summary>
        /// Check to see if the category exists.
        /// </summary>
        /// <param name="name">
        /// The name of the category.
        /// </param>
        /// <returns>
        /// True if the category exists.
        /// </returns>
        public bool Exists(string name)
        {
            using (var db = new MarketDbContainer())
            {
                return db.Categories.Any(c => c.Name == name);
            }
        }

        /// <summary>
        /// Get the unique if of the category.
        /// </summary>
        /// <param name="name">
        /// The name of the category.
        /// </param>
        /// <returns>
        /// The id of the category.
        /// </returns>
        public Guid GetId(string name)
        {
            using (var db = new MarketDbContainer())
            {
                return db.Categories.Single(c => c.Name == name).Id;
            }
        }

        /// <summary>
        /// Save the category.
        /// </summary>
        /// <param name="category">
        /// The category to save.
        /// </param>
        public void Save(Models.Category category)
        {
            using (var db = new MarketDbContainer())
            {
                db.Categories.Add(Map(category));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Save a list of categories.
        /// </summary>
        /// <param name="categories">
        /// The categories to save.
        /// </param>
        public void SaveMany(List<Models.Category> categories)
        {
            using (var db = new MarketDbContainer())
            {
                var repoCategories = categories.Select(x => Map(x));
                db.Categories.AddRange(repoCategories);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Map the category model to the repository model.
        /// </summary>
        /// <param name="category">
        /// The category to map.
        /// </param>
        /// <returns>
        /// The repository category model.
        /// </returns>
        private Category Map(Models.Category category)
        {
            return new Repository.Category
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
