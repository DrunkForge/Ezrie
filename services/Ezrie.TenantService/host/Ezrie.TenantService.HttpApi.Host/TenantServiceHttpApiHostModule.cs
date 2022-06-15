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
using Ezrie.Hosting.AspNetCore;
using Ezrie.Hosting.AspNetCore.Microservices;
using Ezrie.TenantService.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.TenantService;

[DependsOn(typeof(EzrieHostingAspNetCoreMicroservicesModule))]
[DependsOn(typeof(TenantServiceApplicationModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(TenantServiceHttpApiModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
public class TenantServiceHttpApiHostModule : AbpModule
{

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureConventionalControllers();
		ConfigureVirtualFileSystem(context);
	}

	private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainSharedModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Domain.Shared", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Domain", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationContractsModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Application.Contracts", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Application", Path.DirectorySeparatorChar)));
			});
		}
	}

	private void ConfigureConventionalControllers()
	{
		Configure<AbpAspNetCoreMvcOptions>(options =>
		{
			options.ConventionalControllers.Create(typeof(TenantServiceHttpApiHostModule).Assembly);
			options.ConventionalControllers.Create(typeof(TenantServiceApplicationModule).Assembly);
		});
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		IdentityModelEventSource.ShowPII = true;
	}
}
