using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using UserAuth.Data.Core;

namespace WebInkLibrary.Data
{
    public abstract class WiDataContext : UserDbContext, IDbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="nameOrConnnectionString"></param>
        protected WiDataContext(string nameOrConnnectionString) : base(nameOrConnnectionString)
        {
            
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
            //dynamically load all configuration
            //System.Type configType = typeof(LanguageMap);   //any of your configuration classes here
            //var typesToRegister = Assembly.GetAssembly(configType).GetTypes()

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //.Where(type => !String.IsNullOrEmpty(type.Namespace))
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType && //type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
             //   modelBuilder.Configurations.Add(configurationInstance);
            //}
            //...or do it manually below. For example,
            //modelBuilder.Configurations.Add(new LanguageMap());
            //base.OnModelCreating(modelBuilder);
        //}


        // Get DB Set
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }
    }
}
