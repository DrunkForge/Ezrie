namespace Ezrie.RelationshipManagement.Authorization
{
	public class UserInfo
	{
		public static readonly UserInfo Anonymous = new();

		public Boolean IsAuthenticated { get; set; }

		public String NameClaimType { get; set; } = String.Empty;

		public String RoleClaimType { get; set; } = String.Empty;

		public IReadOnlyCollection<ClaimValue> Claims { get; set; } = Array.Empty<ClaimValue>();
	}
}
