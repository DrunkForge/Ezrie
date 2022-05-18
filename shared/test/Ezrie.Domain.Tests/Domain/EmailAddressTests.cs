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

using FluentAssertions;
using System;
using Xunit;

public class EmailAddressTests
{
	[Fact]
	[SuppressMessage("Performance", "CA1806:Do not ignore method results", Justification = "<Pending>")]
	public void Construction()
	{
		// Valid
		new EmailAddress("john.doe@domain.com").User.Should().Be("john.doe");
		new EmailAddress("john-doe1@domain.co").Domain.Should().Be("domain.co");

		//Invalid
		Action act0 = () => new EmailAddress(null!);
		act0.Should().Throw<ArgumentNullException>();

		Action act1 = () => new EmailAddress(String.Empty);
		act1.Should().Throw<ArgumentException>();

		Action act2 = () => new EmailAddress("not.a.valid.email");
		act2.Should().Throw<ArgumentException>();
	}

	[Fact]
	public void Conversion()
	{
		String email = new EmailAddress("somebody@example.com");
		email.Should().Be("somebody@example.com");

		EmailAddress address = "somebody@example.com";
		address.ToString().Should().Be("somebody@example.com");
	}
}
