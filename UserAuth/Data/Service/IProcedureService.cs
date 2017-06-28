using System.Collections.Generic;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Service
{
    public interface IProcedureService
    {
        IEnumerable<Procedure> GetAll();
        Procedure Find(int id);
        Procedure Add(Procedure procedure);
        void Delete(Procedure procedure);
        void Delete(int id);
        Procedure update(Procedure procedure);

    }
}