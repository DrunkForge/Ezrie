using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.IdentityService.Migrations;
using Ezrie.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Ezrie.IdentityService.EntityFrameworkCore;

[ExposeServices(typeof(IIdentityServiceMigrateAndSeedService), typeof(IMigrateAndSeedService))]
public class IdentityServiceMigrateAndSeedService : MigrateAndSeedService<IdentityServiceEntityFrameworkCoreMigrationsModule>, IIdentityServiceMigrateAndSeedService, ITransientDependency
{
	public IdentityServiceMigrateAndSeedService(ILogger<IdentityServiceMigrateAndSeedService> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
	{
	}
}
