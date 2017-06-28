using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Data.Entities.Map
{
    public class ProcedureRoomAssocMap : EntityTypeConfiguration<ProcedureRoomAssoc>
    {
        public ProcedureRoomAssocMap()
        {
            HasKey(e => e.PRAId);
            Property(e => e.PRAId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.MinutesCase).IsOptional();
            Property(e => e.OperatingHours).IsOptional();
            Property(e => e.ScopeExchangeTime).IsOptional();
            Property(e => e.CleansingTime).IsOptional();
            Property(e => e.ProcedureId).IsOptional();
            Property(e => e.RoomId).IsOptional();
            Property(e => e.CreatedOnUtc).IsRequired();
            Property(e => e.UpdatedOnUtc).IsOptional();
            ToTable("ProcedureRoomAssoc");
        }
    }
}