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

#nullable disable

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext : AbpDbContext<AdministrationServiceDbContext>,
	IAdministrationServiceDbContext,
	IFeatureManagementDbContext,
	IPermissionManagementDbContext,
	ISettingManagementDbContext
{
	public AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options)
		: base(options)
	{

	}

	// Feature Management
	public DbSet<FeatureValue> FeatureValues { get; set; }
	// Permission Management
	public DbSet<PermissionGrant> PermissionGrants { get; set; }
	// SettingManagement
	public DbSet<Setting> Settings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.CreateAdministrationServiceModel();
	}
}
