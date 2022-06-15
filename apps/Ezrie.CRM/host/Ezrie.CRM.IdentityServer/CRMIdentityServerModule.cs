using Ezrie.CRM.EntityFrameworkCore;
using Ezrie.MultiTenancy;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Emailing;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI.Navigation.Urls;

namespace Ezrie.CRM;

[DependsOn(typeof(CRMApplicationContractsModule))]
[DependsOn(typeof(CRMEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAccountApplicationModule))]
[DependsOn(typeof(AbpAccountHttpApiModule))]
[DependsOn(typeof(AbpAccountWebIdentityServerModule))]
[DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
[DependsOn(typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(AbpAspNetCoreMvcUiBasicThemeModule))]
[DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpFeatureManagementHttpApiModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(AbpIdentityHttpApiModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpTenantManagementApplicationModule))]
[DependsOn(typeof(AbpTenantManagementHttpApiModule))]
public class CRMIdentityServerModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		context.Services.AddAbpSwaggerGen(
			options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM API", Version = "v1" });
				options.DocInclusionPredicate((docName, description) => true);
				options.CustomSchemaIds(type => type.FullName);
			});

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Languages.Add(new LanguageInfo("en", "en", "English"));
			options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
			options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
		});

		Configure<AbpAuditingOptions>(options =>
		{
			//options.IsEnabledForGetRequests = true;
			options.ApplicationName = "AuthServer";
		});

		Configure<AppUrlOptions>(options =>
		{
			options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
		});

		context.Services.AddAuthentication()
			.AddJwtBearer(options =>
			{
				options.Authority = configuration["AuthServer:Authority"];
				options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"], CultureInfo.InvariantCulture);
				options.Audience = configuration["AuthServer:ApiName"];
			});

		Configure<AbpDistributedCacheOptions>(options =>
		{
			options.KeyPrefix = "CRM:";
		});

		Configure<AbpMultiTenancyOptions>(options =>
		{
			options.IsEnabled = MultiTenancyConsts.IsEnabled;
		});

		var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("ezrie-crm-identity");
		if (!hostingEnvironment.IsDevelopment())
		{
			var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
			dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "CRM-Protection-Keys");
		}

		context.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.WithOrigins(
						configuration["App:CorsOrigins"]
							.Split(",", StringSplitOptions.RemoveEmptyEntries)
							.Select(o => o.RemovePostFix("/"))
							.ToArray()
					)
					.WithAbpExposedHeaders()
					.SetIsOriginAllowedToAllowWildcardSubdomains()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});

#if DEBUG
		context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
	}

	public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		var env = context.GetEnvironment();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseErrorPage();
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseCorrelationId();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseCors();
		app.UseAuthentication();
		app.UseJwtTokenMiddleware();

		if (MultiTenancyConsts.IsEnabled)
		{
			app.UseMultiTenancy();
		}

		app.UseAbpRequestLocalization();
		app.UseIdentityServer();
		app.UseAuthorization();
		app.UseSwagger();
		app.UseAbpSwaggerUI(options =>
		{
			options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
		});
		app.UseAuditing();
		app.UseAbpSerilogEnrichers();
		app.UseConfiguredEndpoints();

		await SeedData(context);
	}

	private static async Task SeedData(ApplicationInitializationContext context)
	{
		using (var scope = context.ServiceProvider.CreateScope())
		{
			await scope.ServiceProvider
				.GetRequiredService<IDataSeeder>()
				.SeedAsync();
		}
	}
}
