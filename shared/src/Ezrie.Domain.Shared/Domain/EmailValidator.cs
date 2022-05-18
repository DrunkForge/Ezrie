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

using System.Text.RegularExpressions;

namespace Ezrie.Domain;

public static class EmailValidator
{
	public const String Pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

	public static readonly Regex Regex = new(Pattern);

	public static Int32 MaximumLength { get; private set; } = 320;

	public static Boolean IsValid(String address)
		=> address != null && address.Length < MaximumLength && Regex.IsMatch(address);

	public static void SetMaximumLength(Int32 maximumLength)
	{
		if (maximumLength < 5 /* a@b.c */)
			throw new ArgumentOutOfRangeException(nameof(maximumLength));

		MaximumLength = maximumLength;
	}
}
