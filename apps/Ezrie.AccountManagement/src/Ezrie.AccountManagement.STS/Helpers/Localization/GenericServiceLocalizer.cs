// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// https://github.com/aspnet/Extensions/blob/master/src/Localization/Abstractions/src/StringLocalizerOfT.cs

using System.Reflection;
using Ezrie.Localization;
using Microsoft.Extensions.Localization;

namespace Ezrie.AccountManagement.STS.Helpers.Localization;

public class GenericControllerLocalizer<TResourceSource> : IGenericControllerLocalizer<TResourceSource>
{
	private IStringLocalizer _localizer;

	/// <summary>
	/// Creates a new <see cref="Microsoft.Extensions.Localization.StringLocalizer`1" />.
	/// </summary>
	/// <param name="factory">The <see cref="IStringLocalizerFactory" /> to use.</param>
	public GenericControllerLocalizer(IStringLocalizerFactory factory)
	{
		if (factory == null)
			throw new ArgumentNullException(nameof(factory));

		var type = typeof(TResourceSource);
		var assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
		if (assemblyName == null)
			throw new LocalizationException($"Could not identify the assembly containing {type}");

		var typeName = type.Name.Remove(type.Name.IndexOf('`', StringComparison.Ordinal));
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

