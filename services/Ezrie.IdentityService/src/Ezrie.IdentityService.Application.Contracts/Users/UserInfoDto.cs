namespace Ezrie.IdentityService.Users
{
	public class UserInfoDto
	{
		public static readonly UserInfoDto Anonymous = new();

		public Boolean IsAuthenticated { get; set; }

		public String NameClaimType { get; set; } = String.Empty;

		public String RoleClaimType { get; set; } = String.Empty;

		public IReadOnlyCollection<ClaimValueDto> Claims { get; set; } = Array.Empty<ClaimValueDto>();
	}
}
