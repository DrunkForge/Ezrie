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

using System.ComponentModel.DataAnnotations;

namespace Ezrie.AccountManagement.Dtos.IdentityResources;

public class IdentityResourceApiDto
{
	public Int32 Id { get; set; }

	[Required]
	public String Name { get; set; } = String.Empty;

	public String DisplayName { get; set; } = String.Empty;

	public String Description { get; set; } = String.Empty;

	public Boolean Enabled { get; set; } = true;

	public Boolean ShowInDiscoveryDocument { get; set; } = true;

	public Boolean Required { get; set; }

	public Boolean Emphasize { get; set; }

	public IReadOnlyCollection<String> UserClaims { get; set; } = Array.Empty<String>();
}

