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

using Ezrie.Validation;

namespace Ezrie.Domain;

public record EmailAddress()
{
	public static readonly EmailAddress Empty = new();

	public EmailAddress(String emailAddress)
		: this()
	{
		ArgumentNullException.ThrowIfNull(emailAddress);

		if (!EmailValidator.IsValid(emailAddress))
			throw new ArgumentException($"'{emailAddress}' is not a valid email address.", nameof(emailAddress));
		var index = emailAddress.IndexOf('@', StringComparison.OrdinalIgnoreCase);
		User = emailAddress[..index];
		Domain = emailAddress[++index..];
	}

	public String User { get; init; } = String.Empty;
	public String Domain { get; init; } = String.Empty;

	public override String ToString() => User.Trim() + "@" + Domain.Trim();

	public static implicit operator String(EmailAddress emailAddress) => emailAddress.ToString();
	public static implicit operator EmailAddress(String emailAddress) => new(emailAddress);

	public static EmailAddress FromString(String emailAddress) => new(emailAddress);
}
