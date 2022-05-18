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

using AutoMapper;
using Ezrie.AccountManagement.Dtos.Roles;
using Ezrie.AccountManagement.Dtos.Users;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;

namespace Ezrie.AccountManagement.Mappers;

public class IdentityMapperProfile<TRoleDto, TUserRolesDto, TKey, TUserClaimsDto, TUserClaimDto, TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimDto, TRoleClaimsDto> : Profile
		where TUserClaimsDto : UserClaimsDto<TUserClaimDto, TKey>
		where TUserClaimDto : UserClaimDto<TKey>
		where TRoleDto : RoleDto<TKey>
		where TUserRolesDto : UserRolesDto<TRoleDto, TKey>
		where TUserProviderDto : UserProviderDto<TKey>
		where TUserProvidersDto : UserProvidersDto<TUserProviderDto, TKey>
		where TUserChangePasswordDto : UserChangePasswordDto<TKey>
		where TRoleClaimsDto : RoleClaimsDto<TRoleClaimDto, TKey>
		where TRoleClaimDto : RoleClaimDto<TKey>
{
	public IdentityMapperProfile()
	{
		// entity to model
		CreateMap<TUserClaimsDto, UserClaimsApiDto<TKey>>(MemberList.Destination);
		CreateMap<TUserClaimsDto, UserClaimApiDto<TKey>>(MemberList.Destination);

		CreateMap<UserClaimApiDto<TKey>, TUserClaimsDto>(MemberList.Source);
		CreateMap<TUserClaimDto, UserClaimApiDto<TKey>>(MemberList.Destination);

		CreateMap<TUserRolesDto, UserRolesApiDto<TRoleDto>>(MemberList.Destination);
		CreateMap<UserRoleApiDto<TKey>, TUserRolesDto>(MemberList.Destination);

		CreateMap<TUserProviderDto, UserProviderApiDto<TKey>>(MemberList.Destination);
		CreateMap<TUserProvidersDto, UserProvidersApiDto<TKey>>(MemberList.Destination);
		CreateMap<UserProviderDeleteApiDto<TKey>, TUserProviderDto>(MemberList.Source);

		CreateMap<UserChangePasswordApiDto<TKey>, TUserChangePasswordDto>(MemberList.Destination);

		CreateMap<RoleClaimsApiDto<TKey>, TRoleClaimsDto>(MemberList.Source);
		CreateMap<RoleClaimApiDto<TKey>, TRoleClaimDto>(MemberList.Destination);
		CreateMap<RoleClaimApiDto<TKey>, TRoleClaimsDto>(MemberList.Destination);

		CreateMap<TRoleClaimsDto, RoleClaimsApiDto<TKey>>(MemberList.Source);
		CreateMap<TRoleClaimDto, RoleClaimApiDto<TKey>>(MemberList.Destination);
		CreateMap<TRoleClaimsDto, RoleClaimApiDto<TKey>>(MemberList.Destination);
	}
}

