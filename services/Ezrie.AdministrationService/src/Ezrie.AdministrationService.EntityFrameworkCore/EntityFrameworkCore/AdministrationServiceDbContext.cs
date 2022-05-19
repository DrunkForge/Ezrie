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
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[ReplaceDbContext(typeof(IAuditLoggingDbContext))]
[ReplaceDbContext(typeof(IFeatureManagementDbContext))]
[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(IPermissionManagementDbContext))]
[ReplaceDbContext(typeof(ISettingManagementDbContext))]
[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext : AbpDbContext<AdministrationServiceDbContext>,
	IAdministrationServiceDbContext,
	IAuditLoggingDbContext,
	IFeatureManagementDbContext,
	IIdentityDbContext,
	IPermissionManagementDbContext,
	ISettingManagementDbContext
{
	/* Add DbSet for each Aggregate Root here. Example:
	 * public DbSet<Question> Questions { get; set; }
	 */

	public AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options)
		: base(options)
	{

	}

	// Audit Logging
	public DbSet<AuditLog> AuditLogs { get; set; }
	// Feature Management
	public DbSet<FeatureValue> FeatureValues { get; set; }
	// Identity
	public DbSet<IdentityUser> Users { get; set; }
	public DbSet<IdentityRole> Roles { get; set; }
	public DbSet<IdentityClaimType> ClaimTypes { get; set; }
	public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
	public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
	public DbSet<IdentityLinkUser> LinkUsers { get; set; }
	// Permission Management
	public DbSet<PermissionGrant> PermissionGrants { get; set; }
	// SettingManagement
	public DbSet<Setting> Settings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ConfigureAdministrationService();
		modelBuilder.ConfigureAuditLogging();
		modelBuilder.ConfigureFeatureManagement();
		modelBuilder.ConfigureIdentity();
		modelBuilder.ConfigurePermissionManagement();
		modelBuilder.ConfigureSettingManagement();
	}
}
