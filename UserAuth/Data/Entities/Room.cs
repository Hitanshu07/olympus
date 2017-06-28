using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuth.Helpers;

namespace UserAuth.Data.Entities
{
    public class Room
    {
        public Room()
        {
            ProcedureRoomAssocs = new List<ProcedureRoomAssoc>();
            CreatedOnUtc = UpdatedOnUtc = AppConstants.MinDateForsql;
        }

        public int RoomId { get; set; }
        public virtual User users { get; set; }
        public int UserId { get; set; }

        public ICollection<ProcedureRoomAssoc> ProcedureRoomAssocs { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}