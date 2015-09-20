namespace PlayDrone2SQL
{
    using System.Collections.Generic;

    public interface IWriter<T>
    {
        /// <summary>
        /// Save an item.
        /// </summary>
        /// <param name="item">
        /// The item to save.
        /// </param>
        void Save(T item);

        /// <summary>
        /// Save many items.
        /// </summary>
        /// <param name="items">
        /// The items to save.
        /// </param>
        void SaveMany(List<T> items);
    }
}
