
namespace PlayDrone2SQL
{
    using System;
    public interface IReader
    {
        int Count();

        Guid GetId(string name);

        bool Exists(string name);
    }
}
