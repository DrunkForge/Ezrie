using System;

namespace CPCA;

public static class CpcaCms
{
	public const String CmsBaseUrl = "https://localhost:54825";
	public const String CdnBaseUrl = "https://cdn.cpca-rpc.ca";
	public const String DirectoryUrl = CmsBaseUrl + "/directory";

	public const Int32 MemberNumberMin = 1100;

	public static class CacheKeys
	{
		private const String CachePrefix = "CpcaCache_";
		public const String DirectoryLookups = CachePrefix + "DirectoryLookups";
	}
}
