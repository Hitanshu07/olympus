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
    public class ProcedureService : IProcedureService
    {
        private readonly UserDbContext _dbContext;

        public ProcedureService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Procedure> GetAll()
        {
            return _dbContext.Procedures.ToList();
        }

        public Procedure Find(int id)
        {
            return _dbContext.Procedures.Find(id);
        }


        public Procedure Add(Procedure procedure)
        {
            procedure.UpdatedOnUtc = procedure.CreatedOnUtc = DateTime.UtcNow;
            _dbContext.Procedures.Add(procedure);
            _dbContext.SaveChanges();
            return procedure;
        }

        public Procedure update(Procedure procedure)
        {
            var oldprocedure = Find(procedure.ProcedureId);
            oldprocedure.CreatedOnUtc = oldprocedure.CreatedOnUtc;
            oldprocedure.IsDisabled = procedure.IsDisabled;
            oldprocedure.ProcedureName = procedure.ProcedureName;
            oldprocedure.UpdatedOnUtc = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return procedure;
        }

        public void Delete(Procedure procedure)
        {
            _dbContext.Procedures.Remove(procedure);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var procedure = Find(id);
            _dbContext.Procedures.Remove(procedure);
            _dbContext.SaveChanges();
        }
    }
}