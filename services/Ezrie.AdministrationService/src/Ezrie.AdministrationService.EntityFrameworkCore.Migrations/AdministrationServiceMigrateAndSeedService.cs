using Ezrie.AdministrationService.EntityFrameworkCore.Migrations;
using Ezrie.AdministrationService.Migrations;
using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[ExposeServices(typeof(IAdministrationServiceMigrateAndSeedService), typeof(IMigrateAndSeedService))]
public class AdministrationServiceMigrateAndSeedService : MigrateAndSeedService<AdministrationServiceEntityFrameworkCoreMigrationsModule>, IAdministrationServiceMigrateAndSeedService, ITransientDependency
{
	public AdministrationServiceMigrateAndSeedService(ILogger<AdministrationServiceMigrateAndSeedService> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
	{
	}
}
