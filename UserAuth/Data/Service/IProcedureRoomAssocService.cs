using System.Collections.Generic;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Service
{
    public interface IProcedureRoomAssocService
    {
        IEnumerable<ProcedureRoomAssoc> GetAll();
        ProcedureRoomAssoc Find(int id);
        ProcedureRoomAssoc Add(ProcedureRoomAssoc procedureroomassoc);
        void Delete(ProcedureRoomAssoc procedureroomassoc);
        void Delete(int id);
        ProcedureRoomAssoc update(ProcedureRoomAssoc procedureroomassoc);

    }
}