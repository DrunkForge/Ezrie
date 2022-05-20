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
using Skoruba.IdentityServer4.Admin.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore;

public class AdminLogDbContext : DbContext, IAdminLogDbContext
{
	public DbSet<Log> Logs { get; set; }

	public AdminLogDbContext(DbContextOptions<AdminLogDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		ConfigureLogContext(modelBuilder);
	}

	private static void ConfigureLogContext(ModelBuilder builder)
	{
		builder.Entity<Log>(log =>
		{
			log.ToTable(TableConsts.Logs);
			log.HasKey(x => x.Id);
			log.Property(x => x.Level).HasMaxLength(128);
		});
	}
}

