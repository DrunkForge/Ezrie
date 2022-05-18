/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Peter Johnson (info@dougwilson.ca)
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

using FluentAssertions;
using Xunit;

namespace Ezrie.Domain;

public class NameTests
{
	[Fact]
	public void Formatted()
	{
		new Name("Peter", "Johnson", Pronouns: "He/Him").ToString().Should().Be("Peter Johnson (He/Him)");
		new Name("Benjamin", "Spock", "M", Honorific: "Dr.", PostNominals: "MD").ToString().Should().Be("Dr. Benjamin M. Spock, MD");
		new Name("Patrick", "Stewart", Honorific: "Sir", PostNominals: "OBE").ToString().Should().Be("Sir Patrick Stewart, OBE");
		new Name("Judy", "Dench", Honorific: "Dame").ToString().Should().Be("Dame Judy Dench");
	}
}
