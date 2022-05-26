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

using Volo.Abp.Localization;

namespace Ezrie.Localization;

[LocalizationResourceName("Ezrie")]
[SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "<Pending>")]
[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
public class EzrieResource
{
	public static class Name
	{
		private const String Prefix = "Name:";
		public static class Label
		{
			private const String Prefix = Name.Prefix + "Label:";
			public const String Honorific = Prefix + "Honorific";
			public const String First = Prefix + "First";
			public const String Initials = Prefix + "Initials";
			public const String Last = Prefix + "Last";
			public const String PostNominals = Prefix + "PostNominals";
		}

		public static class Placeholder
		{
			private const String Prefix = Name.Prefix + "Placeholder:";
			public const String Honorific = Prefix + "Honorific";
			public const String First = Prefix + "First";
			public const String Initials = Prefix + "Initials";
			public const String Last = Prefix + "Last";
			public const String PostNominals = Prefix + "PostNominals";
		}
	}
}
