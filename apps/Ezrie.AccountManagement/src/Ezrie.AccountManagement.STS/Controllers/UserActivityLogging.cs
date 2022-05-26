namespace Ezrie.AccountManagement.STS.Controllers;

public static partial class UserActivityLogging
{
	[LoggerMessage(39, LogLevel.Information, "User with ID {UserId} changed their password successfully.", EventName = nameof(UserChangedPassword))]
	public static partial void UserChangedPassword(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with ID {UserId} asked for their personal data.", EventName = nameof(UserRequestedPersonalData))]
	public static partial void UserRequestedPersonalData(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with ID {UserId} deleted themselves.", EventName = nameof(UserDeletedPersonalData))]
	public static partial void UserDeletedPersonalData(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with ID {UserId} generated new 2FA recovery codes.", EventName = nameof(UserGenerated2FACodes))]
	public static partial void UserGenerated2FACodes(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with ID {UserId} disabled 2fa.", EventName = nameof(UserDisabled2FA))]
	public static partial void UserDisabled2FA(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with id {UserId} reset their authentication app key.", EventName = nameof(UserResetAuthenticationKey))]
	public static partial void UserResetAuthenticationKey(this ILogger logger, string userId);

	[LoggerMessage(39, LogLevel.Information, "User with id {UserId} reset their authentication app key.", EventName = nameof(UserEnabled2FA))]
	public static partial void UserEnabled2FA(this ILogger logger, string userId);
}
