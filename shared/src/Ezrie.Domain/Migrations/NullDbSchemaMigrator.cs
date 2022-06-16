using Volo.Abp.DependencyInjection;

namespace Ezrie.Migrations;

// This is used if database provider doesn't define IDbSchemaMigrator implementation.
public class NullDbSchemaMigrator : IDbSchemaMigrator, ITransientDependency
{
	public Boolean CanMigrate<T>() => false;

	public Task MigrateAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
