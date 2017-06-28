using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuth.Helpers;

namespace UserAuth.Data.Entities
{
    public class ProcedureRoomAssoc
    {
        public ProcedureRoomAssoc()
        {
            CreatedOnUtc = UpdatedOnUtc = AppConstants.MinDateForsql;
        }

        public int PRAId { get; set; }
        public int MinutesPerCase { get; set; }
        public int OperatingHours { get; set; }
        public int ScopeExchangeTime { get; set; }
        public int ExchangeTime { get; set; }

        public virtual Room rooms { get; set; }
        public int RoomId { get; set; }
        public virtual Procedure procedures { get; set; }
        public int ProcedureId { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}