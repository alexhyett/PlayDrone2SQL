
namespace PlayDrone2SQL.Models
{
    using System;

    /// <summary>
    /// The category model.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The category id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The category name.
        /// </summary>
        public string Name { get; set; }
    }
}
