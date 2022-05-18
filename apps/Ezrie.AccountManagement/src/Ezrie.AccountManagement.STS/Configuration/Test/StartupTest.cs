using Ezrie.AccountManagement.EntityFrameworkCore.EntityFrameworkCore;
using Ezrie.AccountManagement.STS.Helpers;

namespace Ezrie.AccountManagement.STS.Configuration.Test;

public class StartupTest : Startup
{
	public StartupTest(IWebHostEnvironment environment, IConfiguration configuration) : base(environment, configuration)
	{
	}

	public override void RegisterDbContexts(IServiceCollection services)
	{
		services.RegisterDbContextsStaging<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, IdentityServerDataProtectionDbContext>();
	}
}

