namespace Ezrie.RelationshipManagement.Authorization;

public class ClaimValue
{
	public ClaimValue()
	{
	}

	public ClaimValue(String type, String value)
	{
		Type = type;
		Value = value;
	}

	public String Type { get; set; } = String.Empty;

	public String Value { get; set; } = String.Empty;
}
