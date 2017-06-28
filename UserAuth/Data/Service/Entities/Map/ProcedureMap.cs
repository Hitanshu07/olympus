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
            HasKey(e => e.ProcecdureId);
            Property(e => e.ProcecdureId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.ProcedureName).IsOptional();
            Property(e => e.IsDisabled).IsOptional();
            Property(e => e.CreatedOnUtc).IsRequired();
            Property(e => e.UpdatedOnUtc).IsOptional();
            ToTable("Procedure");
        }
    }
}