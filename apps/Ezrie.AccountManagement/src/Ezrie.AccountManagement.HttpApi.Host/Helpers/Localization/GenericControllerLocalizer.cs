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

// https://github.com/aspnet/Extensions/blob/master/src/Localization/Abstractions/src/StringLocalizerOfT.cs

using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Ezrie.AccountManagement.Helpers.Localization;

public class GenericControllerLocalizer<TResourceSource> : IGenericControllerLocalizer<TResourceSource>
{
	private IStringLocalizer _localizer;

	/// <summary>
	/// Creates a new <see cref="IStringLocalizerFactory" />.
	/// </summary>
	/// <param name="factory">The <see cref="IStringLocalizerFactory" /> to use.</param>
	public GenericControllerLocalizer(IStringLocalizerFactory factory)
	{
		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		var type = typeof(TResourceSource);
		var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
		if (assemblyName == null)
			throw new InvalidProgramException($"The assembly of `{type.FullName}` does not have a valid Name.  A IStringLocalizerFactory cannot be created for it.");

		var typeName = type.Name.Remove(type.Name.IndexOf('`', StringComparison.OrdinalIgnoreCase));
		var baseName = (type.Namespace + "." + typeName)[assemblyName.Length..].Trim('.');

		_localizer = factory.Create(baseName, assemblyName);
	}

	public virtual LocalizedString this[String name]
	{
		get
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			return _localizer[name];
		}
	}

	public virtual LocalizedString this[String name, params Object[] arguments]
	{
		get
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));
			return _localizer[name, arguments];
		}
	}

	public IEnumerable<LocalizedString> GetAllStrings(Boolean includeParentCultures)
	{
		return _localizer.GetAllStrings(includeParentCultures);
	}
}

