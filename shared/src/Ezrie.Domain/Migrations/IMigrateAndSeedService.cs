namespace Ezrie.Migrations;

public interface IMigrateAndSeedService
{
	Task MigrateAndSeedAsync(CancellationToken cancellationToken = default);
}
