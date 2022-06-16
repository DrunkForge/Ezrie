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

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ezrie.AccountManagement.Configuration.Authorization;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var hasAuthorize = context.MethodInfo.DeclaringType != null
			&& (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
				|| context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

		if (hasAuthorize)
		{
			operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
			operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

			operation.Security = new List<OpenApiSecurityRequirement>
				{
					new OpenApiSecurityRequirement
					{
						[
							new OpenApiSecurityScheme {Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "oauth2"}
							}
						] = new[] { EzrieConstants.SuperAdministratorRole }
					}
				};

		}
	}
}
