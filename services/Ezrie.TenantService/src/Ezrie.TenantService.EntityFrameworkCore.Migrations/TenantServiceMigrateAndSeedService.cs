using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Migrations;
using Ezrie.TenantService.EntityFrameworkCore.Migrations;
using Ezrie.TenantService.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Ezrie.TenantService.EntityFrameworkCore;

[ExposeServices(typeof(ITenantServiceMigrateAndSeedService), typeof(IMigrateAndSeedService))]
public class TenantServiceMigrateAndSeedService : MigrateAndSeedService<TenantServiceEntityFrameworkCoreMigrationsModule>, ITenantServiceMigrateAndSeedService, ITransientDependency
{
	public TenantServiceMigrateAndSeedService(ILogger<TenantServiceMigrateAndSeedService> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
	{
	}
}
