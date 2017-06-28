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
  public class UserService : IUserService
  {
    private readonly UserDbContext _dbContext;

    public UserService(UserDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IEnumerable<User> GetAll()
    {
      return _dbContext.Users.ToList();
    }

    public User Find(int id)
    {
      return _dbContext.Users.Find(id);
    }


    public User Add(User user)
    {
      user.UpdatedOnUtc = user.CreatedOnUtc = DateTime.UtcNow;
      _dbContext.Users.Add(user);
      _dbContext.SaveChanges();
      return user;
    }

    public User update(User user)
    {
        var olduser = Find(user.UserId);
        olduser.City = user.City;
        olduser.CreatedOnUtc = olduser.CreatedOnUtc;
        olduser.Department = user.Department;
        olduser.EmailId = user.EmailId;
        olduser.EndocscopySuites = user.EndocscopySuites;
        olduser.FirstName = user.FirstName;
        olduser.Hospital = user.Hospital;
        olduser.IsDisabled = user.IsDisabled;
        olduser.LastName = user.LastName;
        olduser.Name = user.Name;
        olduser.Rooms = user.Rooms;
        olduser.State = user.State;
        olduser.UpdatedOnUtc = DateTime.UtcNow;
        _dbContext.SaveChanges();
        return user;
    }

    public void Delete(User user)
    {
      _dbContext.Users.Remove(user);
      _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
      var user = Find(id);
      _dbContext.Users.Remove(user);
      _dbContext.SaveChanges();
    }
  }
}