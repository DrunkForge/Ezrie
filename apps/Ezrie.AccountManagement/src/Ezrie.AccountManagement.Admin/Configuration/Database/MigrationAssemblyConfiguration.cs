using System.Reflection;
using Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.Configuration;
using SqlMigrationAssembly = Ezrie.AccountManagement.EntityFrameworkCore.SqlServer.MigrationAssembly;
using MySqlMigrationAssembly = Ezrie.AccountManagement.EntityFrameworkCore.MySql.MigrationAssembly;
using PostgreSQLMigrationAssembly = Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL.MigrationAssembly;

namespace Ezrie.AccountManagement.Admin.Configuration.Database;

public static class MigrationAssemblyConfiguration
    {
        public static String GetMigrationAssemblyByProvider(DatabaseProviderConfiguration databaseProvider)
        {
            return databaseProvider.ProviderType switch
            {
                DatabaseProviderType.SqlServer => typeof(SqlMigrationAssembly).GetTypeInfo().Assembly.GetName().Name,
                DatabaseProviderType.PostgreSQL => typeof(PostgreSQLMigrationAssembly).GetTypeInfo()
                    .Assembly.GetName()
                    .Name,
                DatabaseProviderType.MySql => typeof(MySqlMigrationAssembly).GetTypeInfo().Assembly.GetName().Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

