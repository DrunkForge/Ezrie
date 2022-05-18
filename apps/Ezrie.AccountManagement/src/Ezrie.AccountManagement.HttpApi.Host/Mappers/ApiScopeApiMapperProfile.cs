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
using Ezrie.AccountManagement.Dtos.ApiScopes;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;

namespace Ezrie.AccountManagement.Mappers;

public class ApiScopeApiMapperProfile : Profile
{
	public ApiScopeApiMapperProfile()
	{
		// Api Scopes
		CreateMap<ApiScopesDto, ApiScopesApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ApiScopeDto, ApiScopeApiDto>(MemberList.Destination)
			.ReverseMap();

		// Api Scope Properties
		CreateMap<ApiScopePropertiesDto, ApiScopePropertiesApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ApiScopePropertyDto, ApiScopePropertyApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ApiScopePropertiesDto, ApiScopePropertyApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApiScopePropertyId))
			.ReverseMap();
	}
}

