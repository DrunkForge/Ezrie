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

namespace Ezrie.AccountManagement.Dtos.Users;

public class UserProviderApiDto<TKey>
{
	public TKey UserId { get; set; } = default!;

	public String UserName { get; set; } = String.Empty;

	public String ProviderKey { get; set; } = String.Empty;

	public String LoginProvider { get; set; } = String.Empty;

	public String ProviderDisplayName { get; set; } = String.Empty;
}

