﻿/*********************************************************************************************
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

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Ezrie.AccountManagement.Configuration.Constants;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.Helpers.Localization;
using Ezrie.AccountManagement.Dtos.Roles;
using Ezrie.AccountManagement.ExceptionHandling;

namespace Ezrie.AccountManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[TypeFilter(typeof(ControllerExceptionFilterAttribute))]
[Produces("application/json", "application/problem+json")]
[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
public class RolesController<TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
			TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
			TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto> : ControllerBase
		where TUserDto : UserDto<TKey>, new()
		where TRoleDto : RoleDto<TKey>, new()
		where TUser : IdentityUser<TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TUserToken : IdentityUserToken<TKey>
		where TUsersDto : UsersDto<TUserDto, TKey>
		where TRolesDto : RolesDto<TRoleDto, TKey>
		where TUserRolesDto : UserRolesDto<TRoleDto, TKey>
		where TUserClaimsDto : UserClaimsDto<TUserClaimDto, TKey>, new()
		where TUserProviderDto : UserProviderDto<TKey>
		where TUserProvidersDto : UserProvidersDto<TUserProviderDto, TKey>
		where TUserChangePasswordDto : UserChangePasswordDto<TKey>
		where TRoleClaimsDto : RoleClaimsDto<TRoleClaimDto, TKey>, new()
		where TUserClaimDto : UserClaimDto<TKey>
		where TRoleClaimDto : RoleClaimDto<TKey>
{
	private readonly IIdentityService<TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
		TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
		TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto> _identityService;
	private readonly IGenericControllerLocalizer<UsersController<TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
		TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
		TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto>> _localizer;

	private readonly IMapper _mapper;
	private readonly IApiErrorResources _errorResources;

	public RolesController(IIdentityService<TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
			TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
			TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto> identityService,
		IGenericControllerLocalizer<UsersController<TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
			TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
			TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto>> localizer, IMapper mapper, IApiErrorResources errorResources)
	{
		_identityService = identityService;
		_localizer = localizer;
		_mapper = mapper;
		_errorResources = errorResources;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TRoleDto>> Get(TKey id)
	{
		var role = await _identityService.GetRoleAsync(id.ToString());

		return Ok(role);
	}

	[HttpGet]
	public async Task<ActionResult<TRolesDto>> Get(String searchText, Int32 page = 1, Int32 pageSize = 10)
	{
		var rolesDto = await _identityService.GetRolesAsync(searchText, page, pageSize);

		return Ok(rolesDto);
	}

	[HttpPost]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	public async Task<ActionResult<TRoleDto>> Post([FromBody] TRoleDto role)
	{
		if (!EqualityComparer<TKey>.Default.Equals(role.Id, default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		var (identityResult, roleId) = await _identityService.CreateRoleAsync(role);
		var createdRole = await _identityService.GetRoleAsync(roleId.ToString());

		return CreatedAtAction(nameof(Get), new { id = createdRole.Id }, createdRole);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody] TRoleDto role)
	{
		await _identityService.GetRoleAsync(role.Id.ToString());
		await _identityService.UpdateRoleAsync(role);

		return Ok();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(TKey id)
	{
		var roleDto = new TRoleDto { Id = id };

		await _identityService.GetRoleAsync(id.ToString());
		await _identityService.DeleteRoleAsync(roleDto);

		return Ok();
	}

	[HttpGet("{id}/Users")]
	public async Task<ActionResult<TUsersDto>> GetRoleUsers(String id, String searchText, Int32 page = 1, Int32 pageSize = 10)
	{
		var usersDto = await _identityService.GetRoleUsersAsync(id, searchText, page, pageSize);

		return Ok(usersDto);
	}

	[HttpGet("{id}/Claims")]
	public async Task<ActionResult<RoleClaimsApiDto<TKey>>> GetRoleClaims(String id, Int32 page = 1, Int32 pageSize = 10)
	{
		var roleClaimsDto = await _identityService.GetRoleClaimsAsync(id, page, pageSize);
		var roleClaimsApiDto = _mapper.Map<RoleClaimsApiDto<TKey>>(roleClaimsDto);

		return Ok(roleClaimsApiDto);
	}

	[HttpPost("Claims")]
	public async Task<IActionResult> PostRoleClaims([FromBody] RoleClaimApiDto<TKey> roleClaims)
	{
		var roleClaimsDto = _mapper.Map<TRoleClaimsDto>(roleClaims);

		if (!roleClaimsDto.ClaimId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		await _identityService.CreateRoleClaimsAsync(roleClaimsDto);

		return Ok();
	}

	[HttpPut("Claims")]
	public async Task<IActionResult> PutRoleClaims([FromBody] RoleClaimApiDto<TKey> roleClaims)
	{
		var roleClaimsDto = _mapper.Map<TRoleClaimsDto>(roleClaims);

		if (!roleClaimsDto.ClaimId.Equals(default))
		{
			return BadRequest(_errorResources.CannotSetId());
		}

		await _identityService.UpdateRoleClaimsAsync(roleClaimsDto);

		return Ok();
	}

	[HttpDelete("{id}/Claims")]
	public async Task<IActionResult> DeleteRoleClaims(TKey id, Int32 claimId)
	{
		var roleDto = new TRoleClaimsDto { ClaimId = claimId, RoleId = id };

		await _identityService.GetRoleClaimAsync(roleDto.RoleId.ToString(), roleDto.ClaimId);
		await _identityService.DeleteRoleClaimAsync(roleDto);

		return Ok();
	}
}

