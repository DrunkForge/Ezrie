using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;
using Ezrie.AccountManagement.STS.Configuration.Interfaces;

namespace Ezrie.AccountManagement.STS.Configuration;

public class RootConfiguration : IRootConfiguration
{
	public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
	public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
}

