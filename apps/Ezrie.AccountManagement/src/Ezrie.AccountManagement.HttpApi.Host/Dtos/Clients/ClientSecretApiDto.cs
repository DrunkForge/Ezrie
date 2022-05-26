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
using Skoruba.IdentityServer4.Admin.EntityFramework.Helpers;

namespace Ezrie.AccountManagement.Dtos.Clients;

public class ClientSecretApiDto
{
	[Required]
	public String Type { get; set; } = "SharedSecret";

	public Int32 Id { get; set; }

	public String Description { get; set; } = String.Empty;

	[Required]
	public String Value { get; set; } = String.Empty;

	public String HashType { get; set; } = String.Empty;

	public HashType HashTypeEnum => Enum.TryParse(HashType, true, out HashType result) ? result : Skoruba.IdentityServer4.Admin.EntityFramework.Helpers.HashType.Sha256;

	public DateTime? Expiration { get; set; }
}

