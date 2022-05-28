namespace Ezrie.IdentityService.Users;

public class ClaimValueDto
{
	public ClaimValueDto()
	{
	}

	public ClaimValueDto(String type, String value)
	{
		Type = type;
		Value = value;
	}

	public String Type { get; set; } = String.Empty;

	public String Value { get; set; } = String.Empty;
}
