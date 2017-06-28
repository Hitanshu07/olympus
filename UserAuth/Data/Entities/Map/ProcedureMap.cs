using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Data.Entities.Map
{
    public class ProcedureMap : EntityTypeConfiguration<Procedure>
    {
        public ProcedureMap()
        {
            HasKey(e => e.ProcedureId);
            Property(e => e.ProcedureId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.ProcedureName).IsOptional();
            Property(e => e.IsDisabled).IsOptional();
            Property(e => e.CreatedOnUtc).IsRequired();
            Property(e => e.UpdatedOnUtc).IsOptional();
            HasMany(e => e.ProcedureRoomAssocs).WithRequired(u => u.procedures).HasForeignKey(u => u.ProcedureId);
            ToTable("Procedures");
        }
    }
}