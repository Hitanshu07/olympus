using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using UserAuth.Data.Core;

namespace UserAuth.Data
{
  public class ApplicationDbInitializer : IDatabaseInitializer<UserDbContext>
  {
    public void InitializeDatabase(UserDbContext context)
    {

      //var objectContext =
         // ((IObjectContextAdapter)context).ObjectContext;
      //DeleteExistingTables(objectContext);
      //CreateTables(objectContext);

      Seed(context);
    }

    private void DeleteExistingTables(ObjectContext objectContext)
    {
      foreach (var script in _dropEverythingScript)
      {
        objectContext.ExecuteStoreCommand(script);
      }
    }

    private void CreateTables(ObjectContext objectContext)
    {
      var dataBaseCreateScript =
          objectContext.CreateDatabaseScript();
      //Add Migration History Table
      var migrationTableScript =
          @"
                CREATE TABLE [dbo].[__MigrationHistory](
	                [MigrationId] [nvarchar](150) NOT NULL,
	                [ContextKey] [nvarchar](300) NOT NULL,
	                [Model] [varbinary](max) NOT NULL,
	                [ProductVersion] [nvarchar](32) NOT NULL,
                    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
                (
	                [MigrationId] ASC,
	                [ContextKey] ASC
                ))";
      dataBaseCreateScript += migrationTableScript;
      objectContext.ExecuteStoreCommand(dataBaseCreateScript);
    }

    private void Seed(UserDbContext context)
    {
    }


    #region
    private readonly string[] _dropEverythingScript = {
            @"/*Drop all non-system stored procs */
                DECLARE @name VARCHAR(128)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'P' AND category = 0 ORDER BY [name])

                WHILE @name is not null
                BEGIN
                    SELECT @SQL = 'DROP PROCEDURE [dbo].[' + RTRIM(@name) +']'
                    EXEC (@SQL)
                    PRINT 'Dropped Procedure: ' + @name
                    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'P' AND category = 0 AND [name] > @name ORDER BY [name])
                END
                ",@"

                /* Drop all views */
                DECLARE @name VARCHAR(128)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'V' AND category = 0 ORDER BY [name])

                WHILE @name IS NOT NULL
                BEGIN
                    SELECT @SQL = 'DROP VIEW [dbo].[' + RTRIM(@name) +']'
                    EXEC (@SQL)
                    PRINT 'Dropped View: ' + @name
                    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'V' AND category = 0 AND [name] > @name ORDER BY [name])
                END
                ",@"

                /* Drop all functions */
                DECLARE @name VARCHAR(128)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] IN (N'FN', N'IF', N'TF', N'FS', N'FT') AND category = 0 ORDER BY [name])

                WHILE @name IS NOT NULL
                BEGIN
                    SELECT @SQL = 'DROP FUNCTION [dbo].[' + RTRIM(@name) +']'
                    EXEC (@SQL)
                    PRINT 'Dropped Function: ' + @name
                    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] IN (N'FN', N'IF', N'TF', N'FS', N'FT') AND category = 0 AND [name] > @name ORDER BY [name])
                END
                ",@"

                /* Drop all Foreign Key constraints */
                DECLARE @name VARCHAR(128)
                DECLARE @constraint VARCHAR(254)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)

                WHILE @name is not null
                BEGIN
                    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
                    WHILE @constraint IS NOT NULL
                    BEGIN
                        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint) +']'
                        EXEC (@SQL)
                        PRINT 'Dropped FK Constraint: ' + @constraint + ' on ' + @name
                        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
                    END
                SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
                END
                ",@"

                /* Drop all Primary Key constraints */
                DECLARE @name VARCHAR(128)
                DECLARE @constraint VARCHAR(254)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)

                WHILE @name IS NOT NULL
                BEGIN
                    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
                    WHILE @constraint is not null
                    BEGIN
                        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint)+']'
                        EXEC (@SQL)
                        PRINT 'Dropped PK Constraint: ' + @constraint + ' on ' + @name
                        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
                    END
                SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
                END
                ",@"

                /* Drop all tables */
                DECLARE @name VARCHAR(128)
                DECLARE @SQL VARCHAR(254)

                SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 ORDER BY [name])

                WHILE @name IS NOT NULL
                BEGIN
                    SELECT @SQL = 'DROP TABLE [dbo].[' + RTRIM(@name) +']'
                    EXEC (@SQL)
                    PRINT 'Dropped Table: ' + @name
                    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 AND [name] > @name ORDER BY [name])
                END
                "};
    #endregion
  }
}