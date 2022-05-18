namespace Ezrie.AccountManagement.STS.Configuration;

public static class AccountOptions
{
	public static Boolean AllowLocalLogin { get; set; } = true;
	public static Boolean AllowRememberLogin { get; set; } = true;
	public static TimeSpan RememberMeLoginDuration { get; set; } = TimeSpan.FromDays(30);
	public static Boolean ShowLogoutPrompt { get; set; } = true;
	public static Boolean AutomaticRedirectAfterSignOut { get; set; }
	public static String InvalidCredentialsErrorMessage { get; set; } = "Invalid username or password";
}
