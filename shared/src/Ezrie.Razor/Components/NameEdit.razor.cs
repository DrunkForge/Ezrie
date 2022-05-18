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
using Ezrie.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Ezrie.Components;

public partial class NameEdit
{
	[Inject]
	private IStringLocalizer<EzrieResource> L { get; set; } = null!;

	[Parameter]
	public Name Name { get; set; } = Name.Empty;

	private String First { get; set; } = String.Empty;
	private String Last { get; set; } = String.Empty;
	private String Initials { get; set; } = String.Empty;
	private String Honorific { get; set; } = String.Empty;
	private String PostNominals { get; set; } = String.Empty;

	public Task OnHonorificChanged(String value)
	{
		Name = Name is null
			? new(First, Last, value, Initials, PostNominals)
			: Name with { Honorific = value };

		return Task.CompletedTask;
	}

	private Task OnFirstChanged(String value)
	{
		Name = Name is null
			? new(value, Last, Honorific, Initials, PostNominals)
			: Name with { First = value };

		return Task.CompletedTask;
	}

	private Task OnInitialsChanged(String value)
	{
		Name = Name is null
			? new(First, Last, Honorific, value, PostNominals)
			: Name with { Initials = value };

		return Task.CompletedTask;
	}

	private Task OnLastChanged(String value)
	{
		Name = Name is null
			? new(First, value, Honorific, Initials, PostNominals)
			: Name with { Last = value };

		return Task.CompletedTask;
	}

	private Task OnPostNominalsChanged(String value)
	{
		Name = Name is null
			? new(First, Last, Honorific, Initials, value)
			: Name with { PostNominals = value };

		return Task.CompletedTask;
	}
}
