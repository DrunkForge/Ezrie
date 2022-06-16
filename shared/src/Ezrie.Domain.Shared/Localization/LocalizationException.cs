namespace Ezrie.Localization;

public class LocalizationException : EzrieException
{
	public LocalizationException(String? message) : base(message)
	{
	}

	public LocalizationException(String? message, Exception? innerException) : base(message, innerException)
	{
	}
}
