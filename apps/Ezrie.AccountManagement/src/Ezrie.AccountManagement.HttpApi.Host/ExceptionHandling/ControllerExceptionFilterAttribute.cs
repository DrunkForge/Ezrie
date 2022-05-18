/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.ExceptionHandling;

namespace Ezrie.AccountManagement.ExceptionHandling;

public sealed class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
{
	private const String ErrorKey = "Error";

	public override void OnException(ExceptionContext context)
	{
		if (!(context.Exception is UserFriendlyErrorPageException) &&
			!(context.Exception is UserFriendlyViewException))
			return;

		HandleUserFriendlyViewException(context);
		ProcessException(context);
	}

	private static void SetTraceId(String traceIdentifier, ProblemDetails problemDetails)
	{
		var traceId = Activity.Current?.Id ?? traceIdentifier;
		problemDetails.Extensions["traceId"] = traceId;
	}

	private static void ProcessException(ExceptionContext context)
	{
		var problemDetails = new ValidationProblemDetails(context.ModelState)
		{
			Title = "One or more model validation errors occurred.",
			Status = StatusCodes.Status400BadRequest,
			Instance = context.HttpContext.Request.Path
		};

		SetTraceId(context.HttpContext.TraceIdentifier, problemDetails);

		var exceptionResult = new BadRequestObjectResult(problemDetails)
		{
			ContentTypes = {
					"application/problem+json",
					"application/problem+xml" }
		};

		context.ExceptionHandled = true;
		context.Result = exceptionResult;
	}

	private static void HandleUserFriendlyViewException(ExceptionContext context)
	{
		if (context.Exception is UserFriendlyViewException userFriendlyViewException)
		{
			if (userFriendlyViewException.ErrorMessages != null && userFriendlyViewException.ErrorMessages.Any())
			{
				foreach (var message in userFriendlyViewException.ErrorMessages)
				{
					context.ModelState.AddModelError(message.ErrorKey ?? ErrorKey, message.ErrorMessage);
				}
			}
			else
			{
				context.ModelState.AddModelError(userFriendlyViewException.ErrorKey ?? ErrorKey, context.Exception.Message);
			}
		}

		if (context.Exception is UserFriendlyErrorPageException userFriendlyErrorPageException)
		{
			context.ModelState.AddModelError(userFriendlyErrorPageException.ErrorKey ?? ErrorKey, context.Exception.Message);
		}
	}
}
