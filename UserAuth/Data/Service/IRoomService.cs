using System.Collections.Generic;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Service
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room Find(int id);
        Room Add(Room room);
        void Delete(Room room);
        void Delete(int id);
        Room update(Room room);

    }
}