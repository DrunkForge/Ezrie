using Ezrie.RelationshipManagement.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Ezrie.RelationshipManagement;

public class Startup
{
	public Startup(IConfiguration configuration)
		=> Configuration = configuration;

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllersWithViews();
		services.AddRazorPages();

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			// Configure the context to use Microsoft SQL Server.
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

			// Register the entity sets needed by OpenIddict.
			// Note: use the generic overload if you need
			// to replace the default OpenIddict entities.
			options.UseOpenIddict();
		});

		services.AddDatabaseDeveloperPageExceptionFilter();

		// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
		// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
		services.AddQuartz(options =>
		{
			options.UseMicrosoftDependencyInjectionJobFactory();
			options.UseSimpleTypeLoader();
			options.UseInMemoryStore();
		});

		// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
		services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

		services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders()
				.AddDefaultUI();

		services.Configure<IdentityOptions>(options =>
		{
			// Configure Identity to use the same JWT claims as OpenIddict instead
			// of the legacy WS-Federation claims it uses by default (ClaimTypes),
			// which saves you from doing the mapping in your authorization controller.
			options.ClaimsIdentity.UserNameClaimType = Claims.Name;
			options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
			options.ClaimsIdentity.RoleClaimType = Claims.Role;
			options.ClaimsIdentity.EmailClaimType = Claims.Email;

			// Note: to require account confirmation before login,
			// register an email sender service (IEmailSender) and
			// set options.SignIn.RequireConfirmedAccount to true.
			//
			// For more information, visit https://aka.ms/aspaccountconf.
			options.SignIn.RequireConfirmedAccount = false;
		});

		services
			.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.Audience = "ezrie-relationship-management-api";
				options.Authority = "https://localhost:5500";
				options.RefreshOnIssuerKeyNotFound = true;
				options.RequireHttpsMetadata = true;
				options.ForwardAuthenticate = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddOpenIdConnect(options =>
			{
				options.SignInScheme = JwtBearerDefaults.AuthenticationScheme;
				options.Authority = "https://localhost:5500";
				options.ClientId = "ezrie-relationship-management-api";
				options.ClientSecret = "codeflow_pkce_client_secret";
				options.RequireHttpsMetadata = true;
				options.ResponseType = OpenIdConnectResponseType.Code;
				options.Scope.Add("profile");
				options.Scope.Add("ezrie-relationship-management-api");
				options.SaveTokens = true;
				options.GetClaimsFromUserInfoEndpoint = true;
			});

		services.AddOpenIddict()

			// Register the OpenIddict core components.
			.AddCore(options =>
			{
				// Configure OpenIddict to use the Entity Framework Core stores and models.
				// Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
				options.UseEntityFrameworkCore()
					   .UseDbContext<ApplicationDbContext>();

				// Enable Quartz.NET integration.
				options.UseQuartz();
			})

			// Register the OpenIddict server components.
			.AddServer(options =>
			{
				// Enable the authorization, logout, token and userinfo endpoints.
				options.SetAuthorizationEndpointUris("/connect/authorize")
						  .SetLogoutEndpointUris("/connect/logout")
						  .SetIntrospectionEndpointUris("/connect/introspect")
						  .SetTokenEndpointUris("/connect/token")
						  .SetUserinfoEndpointUris("/connect/userinfo")
						  .SetVerificationEndpointUris("/connect/verify");

				// Mark the "email", "profile" and "roles" scopes as supported scopes.
				options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

				// Note: this sample only uses the authorization code flow but you can enable
				// the other flows if you need to support implicit, password or client credentials.
				options.AllowAuthorizationCodeFlow();

				// Register the signing and encryption credentials.
				options.AddDevelopmentEncryptionCertificate()
					   .AddDevelopmentSigningCertificate();

				// Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
				options.UseAspNetCore()
					   .EnableAuthorizationEndpointPassthrough()
					   .EnableLogoutEndpointPassthrough()
					   .EnableTokenEndpointPassthrough()
					   .EnableUserinfoEndpointPassthrough()
					   .EnableStatusCodePagesIntegration();
			})

			// Register the OpenIddict validation components.
			.AddValidation(options =>
			{
				// Import the configuration from the local OpenIddict server instance.
				options.UseLocalServer();

				// Register the ASP.NET Core host.
				options.UseAspNetCore();
			});

		// Register the worker responsible for seeding the database.
		// Note: in a real world application, this step should be part of a setup script.
		services.AddHostedService<Worker>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseStatusCodePagesWithReExecute("~/error");
			//app.UseExceptionHandler("~/error");

			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			//app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapDefaultControllerRoute();
			endpoints.MapRazorPages();
		});
	}
}
