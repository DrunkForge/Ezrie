
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public abstract class EzrieEntityFrameworkCoreTestModuleBase<T> : AbpModule
	where T : DbContext
{
	private SqliteConnection? _sqliteConnection;

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		_sqliteConnection = CreateDatabaseAndGetConnection();

		Configure<AbpDbContextOptions>(options =>
		{
			options.Configure(abpDbContextConfigurationContext =>
			{
				abpDbContextConfigurationContext.DbContextOptions.UseSqlite(_sqliteConnection);
			});
		});
	}

	private SqliteConnection CreateDatabaseAndGetConnection()
	{
		var connection = new SqliteConnection("Data Source=:memory:");
		connection.Open();

		using var context = CreateDbContext(new DbContextOptionsBuilder<T>().UseSqlite(connection).Options);

		context.GetService<IRelationalDatabaseCreator>().CreateTables();

		return connection;
	}

	public abstract T CreateDbContext(DbContextOptions<T> options);
}
