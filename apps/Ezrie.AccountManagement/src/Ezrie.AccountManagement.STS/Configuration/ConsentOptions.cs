namespace Ezrie.AccountManagement.STS.Configuration;

public static class ConsentOptions
{
	public static Boolean EnableOfflineAccess { get; set; } = true;
	public static String OfflineAccessDisplayName { get; set; } = "Offline Access";
	public static String OfflineAccessDescription { get; set; } = "Access to your applications and resources, even when you are offline";
	public static String MustChooseOneErrorMessage { get; } = "You must pick at least one permission";
	public static String InvalidSelectionErrorMessage { get; } = "Invalid selection";
}

