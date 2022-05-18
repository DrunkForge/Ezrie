using Microsoft.Extensions.Localization;

namespace Ezrie.AccountManagement.STS.Helpers.Localization;

public interface IGenericControllerLocalizer<out T>
{
	LocalizedString this[String name] { get; }

	LocalizedString this[String name, params Object[] arguments] { get; }

	IEnumerable<LocalizedString> GetAllStrings(Boolean includeParentCultures);
}

