using Ezrie.Admin.Migrations;
using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Ezrie.Admin.EntityFrameworkCore;

[ExposeServices(typeof(IAdminMigrateAndSeedService), typeof(IMigrateAndSeedService))]
public class AdminMigrateAndSeedService : MigrateAndSeedService<AdminEntityFrameworkCoreMigrationsModule>, IAdminMigrateAndSeedService, ITransientDependency
{
	public AdminMigrateAndSeedService(ILogger<AdminMigrateAndSeedService> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
	{
	}
}
