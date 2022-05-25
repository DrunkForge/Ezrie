using Ezrie.CMS.Blocks;
using Ezrie.CMS.Pages.DisplayTemplates;
using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ezrie;

[DependsOn(typeof(AbpAutofacModule))]
public class EzrieCmsModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();

		context.Services.AddPiranha(options =>
		{
			options.AddRazorRuntimeCompilation = true;

			options.UseCms();
			options.UseManager();

			options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
			options.UseImageSharp();
			options.UseTinyMCE();
			options.UseMemoryCache();

			var connectionString = configuration.GetConnectionString("Default");
			options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
			options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(connectionString));
		});
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();
		var api = context.ServiceProvider.GetRequiredService<IApi>();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UsePiranha(options =>
		{
			// Initialize Piranha
			App.Init(api);
			App.Blocks.Register<AccordionBlock>();
			App.Blocks.Register<AccordionItemBlock>();
			App.Blocks.Register<CardBlock>();
			App.Blocks.Register<HeadingBlock>();
			App.Blocks.Register<WindowBlock>();
			App.Modules.Manager().Scripts.Add("~/assets/js/cpca-blocks.js");
			App.Modules.Manager().Styles.Add("~/assets/css/cpca-blocks.css");

			// Build content types
			new ContentTypeBuilder(api)
				.AddAssembly(typeof(Program).Assembly)
				.Build()
				.DeleteOrphans();

			// Configure Tiny MCE
			EditorConfig.FromFile("editorconfig.json");

			options.UseManager();
			options.UseTinyMCE();
			options.UseIdentity();
		});
	}
}
