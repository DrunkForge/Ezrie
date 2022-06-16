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

using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezrie.Admin;

public static class AdminDbContextModelCreatingExtensions
{
	public static void CreateAdminModel(this ModelBuilder modelBuilder)
	{
		modelBuilder.ConfigureAdmin();
		modelBuilder.ConfigurePermissionManagement();
		modelBuilder.ConfigureSettingManagement();
		modelBuilder.ConfigureBackgroundJobs();
		modelBuilder.ConfigureAuditLogging();
		modelBuilder.ConfigureIdentity();
		modelBuilder.ConfigureIdentityServer();
		modelBuilder.ConfigureFeatureManagement();
		modelBuilder.ConfigureTenantManagement();
	}

	public static void ConfigureAdmin(this ModelBuilder modelBuilder)
	{
		ArgumentNullException.ThrowIfNull(modelBuilder);

		/* Configure all entities here. Example:

		builder.Entity<Question>(b =>
		{
			//Configure table & schema name
			b.ToTable(AdminDbProperties.DbTablePrefix + "Questions", AdminDbProperties.DbSchema);

			b.ConfigureByConvention();

			//Properties
			b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

			//Relations
			b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

			//Indexes
			b.HasIndex(q => q.CreationTime);
		});
		*/
	}
}
