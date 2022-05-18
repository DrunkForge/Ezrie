namespace Ezrie.AccountManagement.STS.Configuration;

public class CultureConfiguration
{
	public static readonly String[] AvailableCultures = { "en", "fa", "fr", "ru", "sv", "zh", "es", "da", "de", "nl", "fi", "pt" };
	public static readonly String DefaultRequestCulture = "en";

	public ICollection<String> Cultures { get; } = new List<String>();
	public String DefaultCulture { get; set; } = DefaultRequestCulture;
}

