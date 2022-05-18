using Ezrie.AccountManagement.STS.Configuration;
using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;

namespace Ezrie.AccountManagement.STS.Configuration.Interfaces;

public interface IRootConfiguration
{
	AdminConfiguration AdminConfiguration { get; }

	RegisterConfiguration RegisterConfiguration { get; }
}

