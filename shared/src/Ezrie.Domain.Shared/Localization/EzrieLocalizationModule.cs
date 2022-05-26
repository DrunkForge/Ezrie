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

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;

namespace Ezrie.Localization;

[DependsOn(typeof(AbpLocalizationModule))]
public class EzrieLocalizationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		if (context is null)
		{
			throw new ArgumentNullException(nameof(context));
		}

		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Add<EzrieResource>("en")
				.AddBaseTypes(typeof(AbpValidationResource))
				.AddVirtualJson("/Localization/Ezrie");

			options.DefaultResourceType = typeof(EzrieResource);

			options.Languages.Add(new LanguageInfo("en", "en", "English"));
			options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
			options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
			options.Languages.Add(new LanguageInfo("es", "es", "Español"));
		});

		Configure<AbpExceptionLocalizationOptions>(options =>
		{
			options.MapCodeNamespace("Ezrie", typeof(EzrieResource));
		});
	}
}
