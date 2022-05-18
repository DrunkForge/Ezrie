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
using Ezrie.AccountManagement.Dtos.Clients;
using Ezrie.AccountManagement.Configuration.Constants;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.Mappers;
using Ezrie.AccountManagement.ExceptionHandling;

namespace Ezrie.AccountManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
[Produces("application/json", "application/problem+json")]
[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
public class ClientsController : ControllerBase
{
	private readonly IClientService _clientService;
	private readonly IApiErrorResources _errorResources;

	public ClientsController(IClientService clientService, IApiErrorResources errorResources)
	{
		_clientService = clientService;
		_errorResources = errorResources;
	}

	[HttpGet]
	public async Task<ActionResult<ClientsApiDto>> Get(String searchText, Int32 page = 1, Int32 pageSize = 10)
	{
		var clientsDto = await _clientService.GetClientsAsync(searchText, page, pageSize);
		var clientsApiDto = clientsDto.ToClientApiModel<ClientsApiDto>();

		return Ok(clientsApiDto);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ClientApiDto>> Get(Int32 id)
	{
		var clientDto = await _clientService.GetClientAsync(id);
		var clientApiDto = clientDto.ToClientApiModel<ClientApiDto>();

		return Ok(clientApiDto);
	}

	[HttpPost]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Post([FromBody] ClientApiDto client)
	{
		var clientDto = client.ToClientApiModel<ClientDto>();

		if (!clientDto.Id.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var id = await _clientService.AddClientAsync(clientDto);
		client.Id = id;

		return CreatedAtAction(nameof(Get), new { id }, client);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody] ClientApiDto client)
	{
		var clientDto = client.ToClientApiModel<ClientDto>();

		await _clientService.GetClientAsync(clientDto.Id);
		await _clientService.UpdateClientAsync(clientDto, true, true);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Int32 id)
	{
		var clientDto = new ClientDto { Id = id };

		await _clientService.GetClientAsync(clientDto.Id);
		await _clientService.RemoveClientAsync(clientDto);

		return Ok();
	}

	[HttpPost("Clone")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostClientClone([FromBody] ClientCloneApiDto client)
	{
		var clientCloneDto = client.ToClientApiModel<ClientCloneDto>();

		var originalClient = await _clientService.GetClientAsync(clientCloneDto.Id);
		var id = await _clientService.CloneClientAsync(clientCloneDto);
		originalClient.Id = id;

		return CreatedAtAction(nameof(Get), new { id }, originalClient);
	}

	[HttpGet("{id}/Secrets")]
	public async Task<ActionResult<ClientSecretsApiDto>> GetSecrets(Int32 id, Int32 page = 1, Int32 pageSize = 10)
	{
		var clientSecretsDto = await _clientService.GetClientSecretsAsync(id, page, pageSize);
		var clientSecretsApiDto = clientSecretsDto.ToClientApiModel<ClientSecretsApiDto>();

		return Ok(clientSecretsApiDto);
	}

	[HttpGet("Secrets/{secretId}")]
	public async Task<ActionResult<ClientSecretApiDto>> GetSecret(Int32 secretId)
	{
		var clientSecretsDto = await _clientService.GetClientSecretAsync(secretId);
		var clientSecretDto = clientSecretsDto.ToClientApiModel<ClientSecretApiDto>();

		return Ok(clientSecretDto);
	}

	[HttpPost("{id}/Secrets")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostSecret(Int32 id, [FromBody] ClientSecretApiDto clientSecretApi)
	{
		var secretsDto = clientSecretApi.ToClientApiModel<ClientSecretsDto>();
		secretsDto.ClientId = id;

		if (!secretsDto.ClientSecretId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var secretId = await _clientService.AddClientSecretAsync(secretsDto);
		clientSecretApi.Id = secretId;

		return CreatedAtAction(nameof(GetSecret), new { secretId }, clientSecretApi);
	}

	[HttpDelete("Secrets/{secretId}")]
	public async Task<IActionResult> DeleteSecret(Int32 secretId)
	{
		var clientSecret = new ClientSecretsDto { ClientSecretId = secretId };

		await _clientService.GetClientSecretAsync(clientSecret.ClientSecretId);
		await _clientService.DeleteClientSecretAsync(clientSecret);

		return Ok();
	}

	[HttpGet("{id}/Properties")]
	public async Task<ActionResult<ClientPropertiesApiDto>> GetProperties(Int32 id, Int32 page = 1, Int32 pageSize = 10)
	{
		var clientPropertiesDto = await _clientService.GetClientPropertiesAsync(id, page, pageSize);
		var clientPropertiesApiDto = clientPropertiesDto.ToClientApiModel<ClientPropertiesApiDto>();

		return Ok(clientPropertiesApiDto);
	}

	[HttpGet("Properties/{propertyId}")]
	public async Task<ActionResult<ClientPropertyApiDto>> GetProperty(Int32 propertyId)
	{
		var clientPropertiesDto = await _clientService.GetClientPropertyAsync(propertyId);
		var clientPropertyApiDto = clientPropertiesDto.ToClientApiModel<ClientPropertyApiDto>();

		return Ok(clientPropertyApiDto);
	}

	[HttpPost("{id}/Properties")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostProperty(Int32 id, [FromBody] ClientPropertyApiDto clientPropertyApi)
	{
		var clientPropertiesDto = clientPropertyApi.ToClientApiModel<ClientPropertiesDto>();
		clientPropertiesDto.ClientId = id;

		if (!clientPropertiesDto.ClientPropertyId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var propertyId = await _clientService.AddClientPropertyAsync(clientPropertiesDto);
		clientPropertyApi.Id = propertyId;

		return CreatedAtAction(nameof(GetProperty), new { propertyId }, clientPropertyApi);
	}

	[HttpDelete("Properties/{propertyId}")]
	public async Task<IActionResult> DeleteProperty(Int32 propertyId)
	{
		var clientProperty = new ClientPropertiesDto { ClientPropertyId = propertyId };

		await _clientService.GetClientPropertyAsync(clientProperty.ClientPropertyId);
		await _clientService.DeleteClientPropertyAsync(clientProperty);

		return Ok();
	}

	[HttpGet("{id}/Claims")]
	public async Task<ActionResult<ClientClaimsApiDto>> GetClaims(Int32 id, Int32 page = 1, Int32 pageSize = 10)
	{
		var clientClaimsDto = await _clientService.GetClientClaimsAsync(id, page, pageSize);
		var clientClaimsApiDto = clientClaimsDto.ToClientApiModel<ClientClaimsApiDto>();

		return Ok(clientClaimsApiDto);
	}

	[HttpGet("Claims/{claimId}")]
	public async Task<ActionResult<ClientClaimApiDto>> GetClaim(Int32 claimId)
	{
		var clientClaimsDto = await _clientService.GetClientClaimAsync(claimId);
		var clientClaimApiDto = clientClaimsDto.ToClientApiModel<ClientClaimApiDto>();

		return Ok(clientClaimApiDto);
	}

	[HttpPost("{id}/Claims")]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> PostClaim(Int32 id, [FromBody] ClientClaimApiDto clientClaimApiDto)
	{
		var clientClaimsDto = clientClaimApiDto.ToClientApiModel<ClientClaimsDto>();
		clientClaimsDto.ClientId = id;

		if (!clientClaimsDto.ClientClaimId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var claimId = await _clientService.AddClientClaimAsync(clientClaimsDto);
		clientClaimApiDto.Id = claimId;

		return CreatedAtAction(nameof(GetClaim), new { claimId }, clientClaimApiDto);
	}

	[HttpDelete("Claims/{claimId}")]
	public async Task<IActionResult> DeleteClaim(Int32 claimId)
	{
		var clientClaimsDto = new ClientClaimsDto { ClientClaimId = claimId };

		await _clientService.GetClientClaimAsync(claimId);
		await _clientService.DeleteClientClaimAsync(clientClaimsDto);

		return Ok();
	}
}

