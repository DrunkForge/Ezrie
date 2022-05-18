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
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using Ezrie.AccountManagement.Dtos.ApiScopes;
using Ezrie.AccountManagement.Configuration.Constants;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.ExceptionHandling;
using Ezrie.AccountManagement.Mappers;

namespace Ezrie.AccountManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
[Produces("application/json", "application/problem+json")]
[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
public class ApiScopesController : ControllerBase
{
	private readonly IApiErrorResources _errorResources;
	private readonly IApiScopeService _apiScopeService;

	public ApiScopesController(IApiErrorResources errorResources, IApiScopeService apiScopeService)
	{
		_errorResources = errorResources;
		_apiScopeService = apiScopeService;
	}

	[HttpGet]
	public async Task<ActionResult<ApiScopesApiDto>> GetScopes(String search, Int32 page = 1, Int32 pageSize = 10)
	{
		var apiScopesDto = await _apiScopeService.GetApiScopesAsync(search, page, pageSize);
		var apiScopesApiDto = apiScopesDto.ToApiScopeApiModel<ApiScopesApiDto>();

		return Ok(apiScopesApiDto);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ApiScopeApiDto>> GetScope(Int32 id)
	{
		var apiScopesDto = await _apiScopeService.GetApiScopeAsync(id);
		var apiScopeApiDto = apiScopesDto.ToApiScopeApiModel<ApiScopeApiDto>();

		return Ok(apiScopeApiDto);
	}

	[HttpGet("{id}/Properties")]
	public async Task<ActionResult<ApiScopePropertiesApiDto>> GetScopeProperties(Int32 id, Int32 page = 1, Int32 pageSize = 10)
	{
		var apiScopePropertiesDto = await _apiScopeService.GetApiScopePropertiesAsync(id, page, pageSize);
		var apiScopePropertiesApiDto = apiScopePropertiesDto.ToApiScopeApiModel<ApiScopePropertiesApiDto>();

		return Ok(apiScopePropertiesApiDto);
	}

	[HttpPost]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostScope([FromBody] ApiScopeApiDto apiScopeApi)
	{
		var apiScope = apiScopeApi.ToApiScopeApiModel<ApiScopeDto>();

		if (!apiScope.Id.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var id = await _apiScopeService.AddApiScopeAsync(apiScope);
		apiScope.Id = id;

		return CreatedAtAction(nameof(GetScope), new { scopeId = id }, apiScope);
	}

	[HttpPost("{id}/Properties")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostProperty(Int32 id, [FromBody] ApiScopePropertyApiDto apiScopePropertyApi)
	{
		var apiResourcePropertiesDto = apiScopePropertyApi.ToApiScopeApiModel<ApiScopePropertiesDto>();
		apiResourcePropertiesDto.ApiScopeId = id;

		if (!apiResourcePropertiesDto.ApiScopePropertyId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var propertyId = await _apiScopeService.AddApiScopePropertyAsync(apiResourcePropertiesDto);
		apiScopePropertyApi.Id = propertyId;

		return CreatedAtAction(nameof(GetProperty), new { propertyId }, apiScopePropertyApi);
	}

	[HttpGet("Properties/{propertyId}")]
	public async Task<ActionResult<ApiScopePropertyApiDto>> GetProperty(Int32 propertyId)
	{
		var apiScopePropertyAsync = await _apiScopeService.GetApiScopePropertyAsync(propertyId);
		var resourcePropertyApiDto = apiScopePropertyAsync.ToApiScopeApiModel<ApiScopePropertyApiDto>();

		return Ok(resourcePropertyApiDto);
	}

	[HttpDelete("Properties/{propertyId}")]
	public async Task<IActionResult> DeleteProperty(Int32 propertyId)
	{
		var apiScopePropertiesDto = new ApiScopePropertiesDto { ApiScopePropertyId = propertyId };

		await _apiScopeService.GetApiScopePropertyAsync(apiScopePropertiesDto.ApiScopePropertyId);
		await _apiScopeService.DeleteApiScopePropertyAsync(apiScopePropertiesDto);

		return Ok();
	}

	[HttpPut]
	public async Task<IActionResult> PutScope([FromBody] ApiScopeApiDto apiScopeApi)
	{
		var apiScope = apiScopeApi.ToApiScopeApiModel<ApiScopeDto>();

		await _apiScopeService.GetApiScopeAsync(apiScope.Id);

		await _apiScopeService.UpdateApiScopeAsync(apiScope);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteScope(Int32 id)
	{
		var apiScope = new ApiScopeDto { Id = id };

		await _apiScopeService.GetApiScopeAsync(apiScope.Id);

		await _apiScopeService.DeleteApiScopeAsync(apiScope);

		return Ok();
	}
}

