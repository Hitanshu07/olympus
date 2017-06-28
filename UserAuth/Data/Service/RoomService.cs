using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Core;
using UserAuth.Data.Entities;
using UserAuth.Helpers;
using WebGrease.Css.Extensions;

namespace UserAuth.Data.Service
{
    public class RoomService : IRoomService
    {
        private readonly UserDbContext _dbContext;

        public RoomService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Room> GetAll()
        {
            return _dbContext.Rooms.ToList();
        }

        public Room Find(int id)
        {
            return _dbContext.Rooms.Find(id);
        }


        public Room Add(Room rooms)
        {
            rooms.UpdatedOnUtc = rooms.CreatedOnUtc = DateTime.UtcNow;
            _dbContext.Rooms.Add(rooms);
            _dbContext.SaveChanges();
            return rooms;
        }

        public Room update(Room rooms)
        {
            var oldroom = Find(rooms.RoomId);
            oldroom.CreatedOnUtc = oldroom.CreatedOnUtc;
            oldroom.IsDisabled = rooms.IsDisabled;
            oldroom.UserId = rooms.UserId;
            oldroom.UpdatedOnUtc = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return rooms;
        }

        public void Delete(Room room)
        {
            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var room = Find(id);
            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();
        }
    }
}