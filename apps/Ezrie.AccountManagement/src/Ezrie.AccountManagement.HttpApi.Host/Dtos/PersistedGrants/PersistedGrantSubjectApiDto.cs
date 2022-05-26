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

namespace Ezrie.AccountManagement.Dtos.PersistedGrants;

public class PersistedGrantSubjectApiDto
{
	public String Key { get; set; } = String.Empty;
	public String Type { get; set; } = String.Empty;
	public String SubjectId { get; set; } = String.Empty;
	public String SubjectName { get; set; } = String.Empty;
	public String ClientId { get; set; } = String.Empty;
	public DateTime CreationTime { get; set; }
	public DateTime? Expiration { get; set; }
	public String Data { get; set; } = String.Empty;
	public DateTime? ConsumedTime { get; set; }
	public String SessionId { get; set; } = String.Empty;
	public String Description { get; set; } = String.Empty;
}

