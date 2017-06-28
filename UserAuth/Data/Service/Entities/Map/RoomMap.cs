using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Data.Entities.Map
{
    public class RoomMap : EntityTypeConfiguration<Room>
    {
        public RoomMap()
        {
            HasKey(e => e.RoomId);
            Property(e => e.RoomId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.UserId).IsOptional();
            Property(e => e.IsDisabled).IsOptional();
            Property(e => e.CreatedOnUtc).IsRequired();
            Property(e => e.UpdatedOnUtc).IsOptional();
            ToTable("Room");
        }
    }
}