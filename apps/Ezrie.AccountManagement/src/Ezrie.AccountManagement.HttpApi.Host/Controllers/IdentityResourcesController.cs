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
using Ezrie.AccountManagement.Dtos.IdentityResources;
using Ezrie.AccountManagement.Configuration.Constants;
using Ezrie.AccountManagement.Mappers;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.ExceptionHandling;

namespace Ezrie.AccountManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
[Produces("application/json", "application/problem+json")]
[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
public class IdentityResourcesController : ControllerBase
{
	private readonly IIdentityResourceService _identityResourceService;
	private readonly IApiErrorResources _errorResources;

	public IdentityResourcesController(IIdentityResourceService identityResourceService, IApiErrorResources errorResources)
	{
		_identityResourceService = identityResourceService;
		_errorResources = errorResources;
	}

	[HttpGet]
	public async Task<ActionResult<IdentityResourcesApiDto>> Get(String searchText, Int32 page = 1, Int32 pageSize = 10)
	{
		var identityResourcesDto = await _identityResourceService.GetIdentityResourcesAsync(searchText, page, pageSize);
		var identityResourcesApiDto = identityResourcesDto.ToIdentityResourceApiModel<IdentityResourcesApiDto>();

		return Ok(identityResourcesApiDto);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<IdentityResourceApiDto>> Get(Int32 id)
	{
		var identityResourceDto = await _identityResourceService.GetIdentityResourceAsync(id);
		var identityResourceApiModel = identityResourceDto.ToIdentityResourceApiModel<IdentityResourceApiDto>();

		return Ok(identityResourceApiModel);
	}

	[HttpPost]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] IdentityResourceApiDto identityResourceApi)
	{
		var identityResourceDto = identityResourceApi.ToIdentityResourceApiModel<IdentityResourceDto>();

		if (!identityResourceDto.Id.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var id = await _identityResourceService.AddIdentityResourceAsync(identityResourceDto);
		identityResourceApi.Id = id;

		return CreatedAtAction(nameof(Get), new { id }, identityResourceApi);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody] IdentityResourceApiDto identityResourceApi)
	{
		var identityResource = identityResourceApi.ToIdentityResourceApiModel<IdentityResourceDto>();

		await _identityResourceService.GetIdentityResourceAsync(identityResource.Id);
		await _identityResourceService.UpdateIdentityResourceAsync(identityResource);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Int32 id)
	{
		var identityResource = new IdentityResourceDto { Id = id };

		await _identityResourceService.GetIdentityResourceAsync(identityResource.Id);
		await _identityResourceService.DeleteIdentityResourceAsync(identityResource);

		return Ok();
	}

	[HttpGet("{id}/Properties")]
	public async Task<ActionResult<IdentityResourcePropertiesApiDto>> GetProperties(Int32 id, Int32 page = 1, Int32 pageSize = 10)
	{
		var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertiesAsync(id, page, pageSize);
		var identityResourcePropertiesApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertiesApiDto>();

		return Ok(identityResourcePropertiesApiDto);
	}

	[HttpGet("Properties/{propertyId}")]
	public async Task<ActionResult<IdentityResourcePropertyApiDto>> GetProperty(Int32 propertyId)
	{
		var identityResourcePropertiesDto = await _identityResourceService.GetIdentityResourcePropertyAsync(propertyId);
		var identityResourcePropertyApiDto = identityResourcePropertiesDto.ToIdentityResourceApiModel<IdentityResourcePropertyApiDto>();

		return Ok(identityResourcePropertyApiDto);
	}

	[HttpPost("{id}/Properties")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostProperty(Int32 id, [FromBody] IdentityResourcePropertyApiDto identityResourcePropertyApi)
	{
		var identityResourcePropertiesDto = identityResourcePropertyApi.ToIdentityResourceApiModel<IdentityResourcePropertiesDto>();
		identityResourcePropertiesDto.IdentityResourceId = id;

		if (!identityResourcePropertiesDto.IdentityResourcePropertyId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var propertyId = await _identityResourceService.AddIdentityResourcePropertyAsync(identityResourcePropertiesDto);
		identityResourcePropertyApi.Id = propertyId;

		return CreatedAtAction(nameof(GetProperty), new { propertyId }, identityResourcePropertyApi);
	}

	[HttpDelete("Properties/{propertyId}")]
	public async Task<IActionResult> DeleteProperty(Int32 propertyId)
	{
		var identityResourceProperty = new IdentityResourcePropertiesDto { IdentityResourcePropertyId = propertyId };

		await _identityResourceService.GetIdentityResourcePropertyAsync(identityResourceProperty.IdentityResourcePropertyId);
		await _identityResourceService.DeleteIdentityResourcePropertyAsync(identityResourceProperty);

		return Ok();
	}
}

