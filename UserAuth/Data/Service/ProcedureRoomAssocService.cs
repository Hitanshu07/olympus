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
    public class ProcedureRoomAssocService : IProcedureRoomAssocService
    {
        private readonly UserDbContext _dbContext;

        public ProcedureRoomAssocService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProcedureRoomAssoc> GetAll()
        {
            return _dbContext.ProcedureRoomAssocs.ToList();
        }

        public ProcedureRoomAssoc Find(int id)
        {
            return _dbContext.ProcedureRoomAssocs.Find(id);
        }


        public ProcedureRoomAssoc Add(ProcedureRoomAssoc ProcedureRoomAssoc)
        {
            ProcedureRoomAssoc.UpdatedOnUtc = ProcedureRoomAssoc.CreatedOnUtc = DateTime.UtcNow;
            _dbContext.ProcedureRoomAssocs.Add(ProcedureRoomAssoc);
            _dbContext.SaveChanges();
            return ProcedureRoomAssoc;
        }

        public ProcedureRoomAssoc update(ProcedureRoomAssoc procedureRoomAssoc)
        {
            var oldProcedureRoomAssoc = Find(procedureRoomAssoc.PRAId);
            oldProcedureRoomAssoc.ExchangeTime = procedureRoomAssoc.ExchangeTime;
            oldProcedureRoomAssoc.CreatedOnUtc = oldProcedureRoomAssoc.CreatedOnUtc;
            oldProcedureRoomAssoc.MinutesPerCase = procedureRoomAssoc.MinutesPerCase;
            oldProcedureRoomAssoc.OperatingHours = procedureRoomAssoc.OperatingHours;
            oldProcedureRoomAssoc.ProcedureId = procedureRoomAssoc.ProcedureId;
            oldProcedureRoomAssoc.RoomId = procedureRoomAssoc.RoomId;
            oldProcedureRoomAssoc.IsDisabled = procedureRoomAssoc.IsDisabled;
            oldProcedureRoomAssoc.ScopeExchangeTime = procedureRoomAssoc.ScopeExchangeTime;
            oldProcedureRoomAssoc.UpdatedOnUtc = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return procedureRoomAssoc;
        }

        public void Delete(ProcedureRoomAssoc procedureRoomAssoc)
        {
            _dbContext.ProcedureRoomAssocs.Remove(procedureRoomAssoc);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var procedureRoomAssoc = Find(id);
            _dbContext.ProcedureRoomAssocs.Remove(procedureRoomAssoc);
            _dbContext.SaveChanges();
        }
    }
}