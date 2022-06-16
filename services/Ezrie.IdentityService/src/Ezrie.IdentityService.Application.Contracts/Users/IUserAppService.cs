using Volo.Abp.Application.Services;

namespace Ezrie.IdentityService.Users;

public interface IUserAppService : IApplicationService
{
	Task<UserInfoDto> GetAsync();
}
