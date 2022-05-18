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

using Ezrie.Domain;

namespace Ezrie;

public static class StringExtensions
{
	public static String Redact(this String source, Int32 visible = 4)
	{
		ArgumentNullException.ThrowIfNull(source);

		return visible < 0
			? throw new ArgumentOutOfRangeException(nameof(visible), "Must be greater than or equal to 0.")
			: source.Length <= visible
				? new String('*', source.Length)
				: String.Create(source.Length, source, (Span<Char> chars, String s) =>
				{
					for (var i = 0; i < chars.Length; i++)
					{
						chars[i] = i < chars.Length - visible ? '*' : source[i];
					}
				});
	}

	public static Boolean IsValidEmail(this String emailAddress)
		=> EmailValidator.IsValid(emailAddress);
}
