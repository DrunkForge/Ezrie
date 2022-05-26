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
using Microsoft.AspNetCore.Mvc;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Ezrie.AccountManagement.Mappers;
using Ezrie.AccountManagement.Helpers;
using Ezrie.AccountManagement.Configuration.Constants;
using Ezrie.AccountManagement.Dtos.PersistedGrants;
using Ezrie.AccountManagement.ExceptionHandling;
using NUglify.Helpers;

namespace Ezrie.AccountManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
[Produces("application/json")]
[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
public class PersistedGrantsController : ControllerBase
{
	private readonly IPersistedGrantAspNetIdentityService _persistedGrantsService;

	public PersistedGrantsController(IPersistedGrantAspNetIdentityService persistedGrantsService)
	{
		_persistedGrantsService = persistedGrantsService;
	}

	[HttpGet("Subjects")]
	public async Task<ActionResult<PersistedGrantSubjectsApiDto>> Get(String searchText, Int32 page = 1, Int32 pageSize = 10)
	{
		var persistedGrantsDto = await _persistedGrantsService.GetPersistedGrantsByUsersAsync(searchText, page, pageSize);
		var persistedGrantSubjectsApiDto = persistedGrantsDto.ToPersistedGrantApiModel<PersistedGrantSubjectsApiDto>();

		return Ok(persistedGrantSubjectsApiDto);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PersistedGrantApiDto>> Get(String id)
	{
		var persistedGrantDto = await _persistedGrantsService.GetPersistedGrantAsync(UrlHelpers.QueryStringUnSafeHash(id));
		var persistedGrantApiDto = persistedGrantDto.ToPersistedGrantApiModel<PersistedGrantApiDto>();

		ParsePersistedGrantKey(persistedGrantApiDto);

		return Ok(persistedGrantApiDto);
	}

	[HttpGet("Subjects/{subjectId}")]
	public async Task<ActionResult<PersistedGrantsApiDto>> GetBySubject(String subjectId, Int32 page = 1, Int32 pageSize = 10)
	{
		var persistedGrantDto = await _persistedGrantsService.GetPersistedGrantsByUserAsync(subjectId, page, pageSize);
		var persistedGrantApiDto = persistedGrantDto.ToPersistedGrantApiModel<PersistedGrantsApiDto>();

		ParsePersistedGrantKeys(persistedGrantApiDto);

		return Ok(persistedGrantApiDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(String id)
	{
		await _persistedGrantsService.DeletePersistedGrantAsync(UrlHelpers.QueryStringUnSafeHash(id));

		return Ok();
	}

	[HttpDelete("Subjects/{subjectId}")]
	public async Task<IActionResult> DeleteBySubject(String subjectId)
	{
		await _persistedGrantsService.DeletePersistedGrantsAsync(subjectId);

		return Ok();
	}

	private static void ParsePersistedGrantKey(PersistedGrantApiDto persistedGrantApiDto)
	{
		if (!String.IsNullOrEmpty(persistedGrantApiDto.Key))
		{
			persistedGrantApiDto.Key = UrlHelpers.QueryStringSafeHash(persistedGrantApiDto.Key);
		}
	}

	private static void ParsePersistedGrantKeys(PersistedGrantsApiDto persistedGrantApiDto)
	{
		persistedGrantApiDto.PersistedGrants.ForEach(ParsePersistedGrantKey);
	}
}

