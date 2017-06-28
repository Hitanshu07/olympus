using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAuth.Helpers;

namespace UserAuth.Data.Entities
{
    public class Calculation
    {
        public Calculation()
        {
            CreatedOnUtc = UpdatedOnUtc = AppConstants.MinDateForsql;
        }

        public int CalculationId { get; set; }
        public int Time1Scope { get; set; }
        public int Time2Scope { get; set; }
        public int Time3Scope { get; set; }
        public int Time4Scope { get; set; }
        public int Time5Scope { get; set; }

        public virtual ProcedureRoomAssoc procedureRoomAssocs { get; set; }
        public int PRAId { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}