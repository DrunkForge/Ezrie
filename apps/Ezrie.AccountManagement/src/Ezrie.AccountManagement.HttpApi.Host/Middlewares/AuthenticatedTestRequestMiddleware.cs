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

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ezrie.AccountManagement.Middlewares;

public class AuthenticatedTestRequestMiddleware
{
	private readonly RequestDelegate _next;
	public static readonly String TestAuthorizationHeader = "FakeAuthorization";
	public AuthenticatedTestRequestMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		if (context.Request.Headers.ContainsKey(TestAuthorizationHeader))
		{
			var token = context.Request.Headers[TestAuthorizationHeader].Single();
			var jwt = new JwtSecurityToken(token);
			var claimsIdentity = new ClaimsIdentity(jwt.Claims, JwtBearerDefaults.AuthenticationScheme, JwtClaimTypes.Name, JwtClaimTypes.Role);
			context.User = new ClaimsPrincipal(claimsIdentity);
		}

		await _next(context);
	}
}

