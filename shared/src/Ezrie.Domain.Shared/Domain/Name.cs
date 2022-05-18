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

namespace Ezrie.Domain;

public record Name(String First, String Last, String Initials = "", String Pronouns = "", String Honorific = "", String PostNominals = "")
{
	public static readonly String[] Honorifics = new[] { "Sir", "Lady", "Dame" };

	public static readonly Name Empty = new(String.Empty, String.Empty, Pronouns: String.Empty);

	public String Sort { get; init; } = String.Empty;

	public override String ToString() => String.Join(null, Components());

	public static implicit operator String(Name name) => name.ToString();

	private IEnumerable<String> Components()
	{
		yield return String.IsNullOrWhiteSpace(Honorific)
			? String.Empty
			: Honorifics.Contains(Honorific)
				? $"{Honorific} "
				: $"{Honorific.EnsureEndsWith('.')} ";

		yield return $"{First} ";

		yield return String.IsNullOrWhiteSpace(Initials)
			? String.Empty
			: $"{Initials.EnsureEndsWith('.')} ";

		yield return $"{Last}";

		yield return String.IsNullOrWhiteSpace(Pronouns)
			? String.Empty
			: $" ({Pronouns})";

		yield return String.IsNullOrWhiteSpace(PostNominals)
			? String.Empty
			: $", {PostNominals}";
	}
}
