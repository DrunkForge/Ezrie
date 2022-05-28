using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Volo.Abp;

namespace Ezrie.IdentityService.Users;

[Area(IdentityServiceRemoteServiceConsts.ModuleName)]
[RemoteService(Name = IdentityServiceRemoteServiceConsts.RemoteServiceName)]
[Route("api/identity/user")]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;

	public UserController(ILogger<UserController> logger) => _logger = logger;

	[HttpGet("current")]
	public IActionResult CurrentUser()
	{
		_logger.LogInformation("Generating claims for {User}", User.Identity?.Name ?? "Anonymous");
		return Ok(User.Identity?.IsAuthenticated == true ? CreateUserInfoDto(User) : UserInfoDto.Anonymous);
	}

	private UserInfoDto CreateUserInfoDto(ClaimsPrincipal claimsPrincipal)
	{
		if (claimsPrincipal.Identity?.IsAuthenticated == false)
		{
			return UserInfoDto.Anonymous;
		}

		var userInfo = new UserInfoDto
		{
			IsAuthenticated = true
		};

		if (claimsPrincipal.Identity is ClaimsIdentity claimsIdentity)
		{
			userInfo.NameClaimType = claimsIdentity.NameClaimType;
			userInfo.RoleClaimType = claimsIdentity.RoleClaimType;
		}
		else
		{
			userInfo.NameClaimType = JwtClaimTypes.Name;
			userInfo.RoleClaimType = JwtClaimTypes.Role;
		}

		if (claimsPrincipal.Claims.Any())
		{
			var claims = new List<ClaimValueDto>();
			var nameClaims = claimsPrincipal.FindAll(userInfo.NameClaimType);
			foreach (var claim in nameClaims)
			{
				claims.Add(new ClaimValueDto(userInfo.NameClaimType, claim.Value));
				_logger.LogDebug("Found claim for user {User}: {Type}={Value}", User.Identity?.Name, claim.Type, claim.Value);
			}

			// Uncomment this code if you want to send additional claims to the client.
			//foreach (var claim in claimsPrincipal.Claims.Except(nameClaims))
			//{
			//    claims.Add(new ClaimValueDto(claim.Type, claim.Value));
			//}

			userInfo.Claims = claims;
		}

		return userInfo;
	}
}
