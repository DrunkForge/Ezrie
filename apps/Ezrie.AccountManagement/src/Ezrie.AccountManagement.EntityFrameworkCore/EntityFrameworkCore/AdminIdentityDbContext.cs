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

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ezrie.AccountManagement.Identity;

namespace Ezrie.AccountManagement.EntityFrameworkCore;

public class AdminIdentityDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, String, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
{
	public AdminIdentityDbContext(DbContextOptions<AdminIdentityDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		ConfigureIdentityContext(builder);
	}

	private static void ConfigureIdentityContext(ModelBuilder builder)
	{
		builder.Entity<UserIdentityRole>().ToTable(TableConsts.IdentityRoles);
		builder.Entity<UserIdentityRoleClaim>().ToTable(TableConsts.IdentityRoleClaims);
		builder.Entity<UserIdentityUserRole>().ToTable(TableConsts.IdentityUserRoles);

		builder.Entity<UserIdentity>().ToTable(TableConsts.IdentityUsers);
		builder.Entity<UserIdentityUserLogin>().ToTable(TableConsts.IdentityUserLogins);
		builder.Entity<UserIdentityUserClaim>().ToTable(TableConsts.IdentityUserClaims);
		builder.Entity<UserIdentityUserToken>().ToTable(TableConsts.IdentityUserTokens);
	}
}

