using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Auth;

[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is a clean and tidy way for managing constants. -dw")]
public static class AuthProperties
{
	public static class OidcScopes
	{
		public const String Address = "address";
		public const String Email = "email";
		public const String OfflineAccess = "offline_access";
		public const String OpenId = "openid";
		public const String Phone = "phone";
		public const String Profile = "profile";
		public const String Role = "role";

		public const String Administration = "ezrie-administration-service-api";
		public const String Identity = "ezrie-identity-service-api";
		public const String Tenants = "ezrie-tenant-service-api";
	}

	public static class OidcClients
	{
		public const String CrmApp = "ezrie-crm-app";
	}
}
