using System;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Core
{
  public class UserDbContext: DbContext
  {
    private const string DefaultConnectionString = "name=DefaultConnection";

    public UserDbContext(string connectionString)
      : base(connectionString ?? DefaultConnectionString)
    {
      var initEnabledString = ConfigurationManager.AppSettings["InitializeDB"];
      bool initEnabled;
      if (!String.IsNullOrEmpty(initEnabledString) && bool.TryParse(initEnabledString, out initEnabled) && initEnabled)
        Database.SetInitializer(new ApplicationDbInitializer());
    }
    public UserDbContext()
        : base("name=DefaultConnection")
    {
    }

    public IDbSet<User> Users { get; set; }
    public IDbSet<Calculation> Calculations { get; set; }
    public IDbSet<Procedure> Procedures { get; set; }
    public IDbSet<ProcedureRoomAssoc> ProcedureRoomAssocs { get; set; }
    public IDbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
      base.OnModelCreating(modelBuilder);
    }
    public static UserDbContext Create(string connectionString = null)
    {
      return new UserDbContext(connectionString ?? DefaultConnectionString);
    }
  }

}