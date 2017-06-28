using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;

namespace UserAuth.Data.Entities.Map
{
  public class UserMap: EntityTypeConfiguration<User>
  {
    public UserMap()
    {
      HasKey(e => e.UserId);
      Property(e => e.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      Property(e => e.FirstName).IsOptional();
      Property(e => e.LastName).IsOptional();
      Property(e => e.EmailId).IsOptional();
      Property(e => e.Department).IsOptional();
      Property(e => e.Hospital).IsOptional();
      Property(e => e.City).IsOptional();
      Property(e => e.State).IsOptional();
      Property(e => e.EndoscopicSuite).IsOptional();
      Property(e => e.IsDisabled).IsOptional();
      Property(e => e.CreatedOnUtc).IsRequired();
      Property(e => e.UpdatedOnUtc).IsOptional();
      ToTable("Users");
    }
  }
}