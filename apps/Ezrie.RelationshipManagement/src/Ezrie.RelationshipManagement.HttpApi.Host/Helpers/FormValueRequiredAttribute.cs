using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Ezrie.RelationshipManagement.Helpers;

public sealed class FormValueRequiredAttribute : ActionMethodSelectorAttribute
{
	public FormValueRequiredAttribute(String name)
	{
		Name = name;
	}

	public override Boolean IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
	{
		if (String.Equals(routeContext.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase) ||
			String.Equals(routeContext.HttpContext.Request.Method, "HEAD", StringComparison.OrdinalIgnoreCase) ||
			String.Equals(routeContext.HttpContext.Request.Method, "DELETE", StringComparison.OrdinalIgnoreCase) ||
			String.Equals(routeContext.HttpContext.Request.Method, "TRACE", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}

		if (String.IsNullOrEmpty(routeContext.HttpContext.Request.ContentType))
		{
			return false;
		}

		if (!routeContext.HttpContext.Request.ContentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}

		return !String.IsNullOrEmpty(routeContext.HttpContext.Request.Form[Name]);
	}

	public String Name { get; }
}
