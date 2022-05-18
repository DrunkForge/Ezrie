using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ezrie.AccountManagement.STS.Configuration;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using Serilog;
using Ezrie.AccountManagement.Identity;
using Ezrie.AccountManagement.EntityFrameworkCore.EntityFrameworkCore;
using Ezrie.AccountManagement.STS.Helpers;
using Ezrie.AccountManagement.STS.Configuration.Interfaces;
using Ezrie.AccountManagement.STS.Configuration.Constants;

namespace Ezrie.AccountManagement.STS;

public class Startup
{
	public IConfiguration Configuration { get; }
	public IWebHostEnvironment Environment { get; }

	public Startup(IWebHostEnvironment environment, IConfiguration configuration)
	{
		Configuration = configuration;
		Environment = environment;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		var rootConfiguration = CreateRootConfiguration();
		services.AddSingleton(rootConfiguration);
		// Register DbContexts for IdentityServer and Identity
		RegisterDbContexts(services);

		// Save data protection keys to db, using a common application name shared between Admin and STS
		services.AddDataProtection<IdentityServerDataProtectionDbContext>(Configuration);

		// Add email senders which is currently setup for SendGrid and SMTP
		services.AddEmailSenders(Configuration);

		// Add services for authentication, including Identity model and external providers
		RegisterAuthentication(services);

		// Add HSTS options
		RegisterHstsOptions(services);

		// Add all dependencies for Asp.Net Core Identity in MVC - these dependencies are injected into generic Controllers
		// Including settings for MVC and Localization
		// If you want to change primary keys or use another db model for Asp.Net Core Identity:
		services.AddMvcWithLocalization<UserIdentity, String>(Configuration);

		// Add authorization policies for MVC
		RegisterAuthorization(services);

		services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext, IdentityServerDataProtectionDbContext>(Configuration);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseSerilogRequestLogging();

		app.UseCookiePolicy();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseHsts();
		}

		app.UsePathBase(Configuration.GetValue<String>("BasePath"));

		app.UseStaticFiles();
		app.UseIdentityServer();

		// Add custom security headers
		app.UseSecurityHeaders(Configuration);

		app.UseMvcLocalizationServices();

		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(endpoint =>
		{
			endpoint.MapDefaultControllerRoute();
			endpoint.MapHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
		});
	}

	public virtual void RegisterDbContexts(IServiceCollection services)
	{
		services.RegisterDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, IdentityServerDataProtectionDbContext>(Configuration);
	}

	public virtual void RegisterAuthentication(IServiceCollection services)
	{
		services.AddAuthenticationServices<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
		services.AddIdentityServer<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, UserIdentity>(Configuration);
	}

	public virtual void RegisterAuthorization(IServiceCollection services)
	{
		var rootConfiguration = CreateRootConfiguration();
		services.AddAuthorizationPolicies(rootConfiguration);
	}

	public virtual void RegisterHstsOptions(IServiceCollection services)
	{
		services.AddHsts(options =>
		{
			options.Preload = true;
			options.IncludeSubDomains = true;
			options.MaxAge = TimeSpan.FromDays(365);
		});
	}

	protected IRootConfiguration CreateRootConfiguration()
	{
		var rootConfiguration = new RootConfiguration();
		Configuration.GetSection(ConfigurationConsts.AdminConfigurationKey).Bind(rootConfiguration.AdminConfiguration);
		Configuration.GetSection(ConfigurationConsts.RegisterConfigurationKey).Bind(rootConfiguration.RegisterConfiguration);
		return rootConfiguration;
	}
}
