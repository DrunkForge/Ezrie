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
using Skoruba.AuditLogging.EntityFramework.DbContexts;
using Skoruba.AuditLogging.EntityFramework.Entities;

#nullable disable

namespace Ezrie.AccountManagement.EntityFrameworkCore.EntityFrameworkCore;

public class AdminAuditLogDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
{
	public AdminAuditLogDbContext(DbContextOptions<AdminAuditLogDbContext> dbContextOptions)
		: base(dbContextOptions)
	{

	}

	public Task<Int32> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}

	public DbSet<AuditLog> AuditLog { get; set; }
}

