/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using Ezrie.AdministrationService.EntityFrameworkCore;
using Ezrie.Hosting.AspNetCore.Microservices;
using Ezrie.IdentityService;
using Ezrie.IdentityService.EntityFrameworkCore;
using Ezrie.TenantService;
using Ezrie.TenantService.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(EzrieHostingAspNetCoreMicroservicesModule))]
[DependsOn(typeof(AdministrationServiceApplicationModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(AdministrationServiceHttpApiModule))]
[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(IdentityServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(TenantServiceApplicationContractsModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpIdentityDomainModule))]
public class AdministrationServiceHttpApiHostModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureVirtualFileSystem(context);
		ConfigureConventionalControllers();
	}

	private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<AdministrationServiceDomainSharedModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.AdministrationService.Domain.Shared", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<AdministrationServiceDomainModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.AdministrationService.Domain", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<AdministrationServiceApplicationContractsModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.AdministrationService.Application.Contracts", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<AdministrationServiceApplicationModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.AdministrationService.Application", Path.DirectorySeparatorChar)));
			});
		}
	}

	private void ConfigureConventionalControllers()
	{
		Configure<AbpAspNetCoreMvcOptions>(options =>
		{
			options.ConventionalControllers.Create(typeof(AdministrationServiceHttpApiHostModule).Assembly);
			options.ConventionalControllers.Create(typeof(AdministrationServiceApplicationModule).Assembly);
		});
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		IdentityModelEventSource.ShowPII = true;
	}
}
