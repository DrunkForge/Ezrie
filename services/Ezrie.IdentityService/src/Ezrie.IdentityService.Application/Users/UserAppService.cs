namespace Ezrie.IdentityService.Users;

public class UserAppService : IdentityServiceAppService, IUserAppService
{
	public Task<UserInfoDto> GetAsync() => throw new NotImplementedException();
}
