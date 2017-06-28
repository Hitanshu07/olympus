using System.Collections.Generic;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Service
{
  public interface IUserService
  {
    IEnumerable<User> GetAll();
    User Find(int id);
    User Add(User user);
    void Delete(User user);
    void Delete(int id);
    User update(User user);

  }
}