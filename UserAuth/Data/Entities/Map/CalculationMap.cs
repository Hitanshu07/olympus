using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Data.Entities.Map
{
    public class CalculationMap : EntityTypeConfiguration<Calculation>
    {
        public CalculationMap()
        {
            HasKey(e => e.CalculationId);
            Property(e => e.CalculationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Time1Scope).IsOptional();
            Property(e => e.Time2Scope).IsOptional();
            Property(e => e.Time3Scope).IsOptional();
            Property(e => e.Time4Scope).IsOptional();
            Property(e => e.Time5Scope).IsOptional();
            Property(e => e.PRAId).IsOptional();
            Property(e => e.IsDisabled).IsOptional();
            Property(e => e.CreatedOnUtc).IsRequired();
            Property(e => e.UpdatedOnUtc).IsOptional();
            ToTable("Calculation");
        }
    }
}