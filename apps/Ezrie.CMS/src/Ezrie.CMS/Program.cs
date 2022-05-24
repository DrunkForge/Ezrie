using CPCA;
using CPCA.Presentation.CMS.Blocks;
using CPCA.Presentation.CMS.Pages.DisplayTemplates;
using Microsoft.EntityFrameworkCore;
using Piranha;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

var builder = WebApplication.CreateBuilder(args);

builder.AddCpcaCms();

builder.AddPiranha(options =>
{
	options.AddRazorRuntimeCompilation = true;

	options.UseCms();
	options.UseManager();

	options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
	options.UseImageSharp();
	options.UseTinyMCE();
	options.UseMemoryCache();

	var connectionString = builder.Configuration.GetConnectionString(CpcaConsts.WebsiteDatabase);
	options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
	options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(connectionString));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UsePiranha(options =>
{
	// Initialize Piranha
	App.Init(options.Api);
	App.Blocks.Register<AccordionBlock>();
	App.Blocks.Register<AccordionItemBlock>();
	App.Blocks.Register<CardBlock>();
	App.Blocks.Register<HeadingBlock>();
	App.Blocks.Register<WindowBlock>();
	App.Modules.Manager().Scripts.Add("~/assets/js/cpca-blocks.js");
	App.Modules.Manager().Styles.Add("~/assets/css/cpca-blocks.css");

	// Build content types
	new ContentTypeBuilder(options.Api)
		.AddAssembly(typeof(Program).Assembly)
		.Build()
		.DeleteOrphans();

	// Configure Tiny MCE
	EditorConfig.FromFile("editorconfig.json");

	options.UseManager();
	options.UseTinyMCE();
	options.UseIdentity();
});

app.Run();
