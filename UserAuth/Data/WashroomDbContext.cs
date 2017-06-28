using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using WebInkLibrary.Data;

namespace Module.Washroom.Data
{
  public  class WashroomDbContext:WiDataContext
    {
      public WashroomDbContext()
            : base(null)
        {

        }

//NOTE: Base already does assembly scanning
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            //dynamically load all configuration
//            //System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
//            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
//
//            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
//            .Where(type => !String.IsNullOrEmpty(type.Namespace))
//            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
//            foreach (var type in typesToRegister)
//            {
//                dynamic configurationInstance = Activator.CreateInstance(type);
//                modelBuilder.Configurations.Add(configurationInstance);
//            }
//            //...or do it manually below. For example,
//            //modelBuilder.Configurations.Add(new LanguageMap());
//            base.OnModelCreating(modelBuilder);
//        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
