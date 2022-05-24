using BlazorAppWithIdentity.Server.Data;
using BlazorAppWithIdentity.Server.Models;
using Ezrie;
using Ezrie.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace BlazorAppWithIdentity.Server;

public class Program
{
	private static IConfiguration GetConfiguration(String[] args)
	{
		var builder = new ConfigurationBuilder()
			.AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
			.AddJsonFile("identitydata.json", optional: true, reloadOnChange: true)
			.AddJsonFile("identityserverdata.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"serilog.{HostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"identitydata.{HostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"identityserverdata.{HostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

		if (HostEnvironment.IsDevelopment)
		{
			builder.AddUserSecrets<Program>(true);
		}

		builder.AddEnvironmentVariables()
			.AddCommandLine(args);

		return builder.Build();
	}

	public static void Main(String[] args)
	{
		var configuration = GetConfiguration(args);
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddEzrieLogging<BlazorAppWithIdentityServerModule>(configuration);

		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		builder.Services
			.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

		builder.Services
			.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services
			.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
			.AddEntityFrameworkStores<ApplicationDbContext>();

		builder.Services
			.AddIdentityServer()
			.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

		builder.Services
			.AddAuthentication()
			.AddIdentityServerJwt();

		builder.Services
			.AddAuthentication(sharedOptions =>
			{
				sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
			.AddCookie()
			.AddOpenIdConnect(o =>
			{
				o.ClientId = "blazorappwithidentity-server";
				o.ClientSecret = "1q2w3E*"; // for code flow
				o.Authority = "https://localhost:5500/";

				o.ResponseType = OpenIdConnectResponseType.CodeIdToken;
				o.SaveTokens = true;
				o.GetClaimsFromUserInfoEndpoint = true;
				o.AccessDeniedPath = "/access-denied-from-remote";
				o.MapInboundClaims = false;
				// o.ClaimActions.MapAllExcept("aud", "iss", "iat", "nbf", "exp", "aio", "c_hash", "uti", "nonce");

				o.Events = new OpenIdConnectEvents()
				{
					OnAuthenticationFailed = c =>
					{
						c.HandleResponse();

						c.Response.StatusCode = 500;
						c.Response.ContentType = "text/plain";
						if (HostEnvironment.IsDevelopment)
						{
							// Debug only, in production do not share exceptions with the remote host.
							return c.Response.WriteAsync(c.Exception.ToString());
						}
						return c.Response.WriteAsync("An error occurred processing your authentication.");
					}
				};
			});

		builder.Services
				.AddControllersWithViews();

		builder.Services
			.AddRazorPages();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseIdentityServer();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapRazorPages();
		app.MapControllers();
		app.MapFallbackToFile("index.html");

		app.Run();
	}
}
