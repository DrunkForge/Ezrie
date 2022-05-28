using Shouldly;
using Xunit;

namespace Ezrie.IdentityService.Users;

public class UsersAppService_Tests : IdentityServiceApplicationTestBase
{
	private readonly IUserAppService _usersAppService;

	public UsersAppService_Tests()
	{
		_usersAppService = GetRequiredService<IUserAppService>();
	}

	[Fact]
	public async Task GetAsync()
	{
		var result = await _usersAppService.GetAsync();
	}
}
