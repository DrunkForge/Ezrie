namespace Ezrie;

public static class EzrieCms
{
	public const String CmsBaseUrl = "https://localhost:54825";
	public const String CdnBaseUrl = "https://cdn.cpca-rpc.ca";
	public const String DirectoryUrl = CmsBaseUrl + "/directory";

	public const Int32 MemberNumberMin = 1100;

	[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is a clean and tidy way for managing constants. -dw")]
	public static class CacheKeys
	{
		private const String CachePrefix = "EzrieCache_";
		public const String DirectoryLookups = CachePrefix + "DirectoryLookups";
	}
}
