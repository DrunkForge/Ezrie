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
using Ezrie.AccountManagement.Dtos.ApiResources;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;

namespace Ezrie.AccountManagement.Mappers;

public class ApiResourceApiMapperProfile : Profile
{
	public ApiResourceApiMapperProfile()
	{
		// Api Resources
		CreateMap<ApiResourcesDto, ApiResourcesApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ApiResourceDto, ApiResourceApiDto>(MemberList.Destination)
			.ReverseMap();

		// Api Secrets
		CreateMap<ApiSecretsDto, ApiSecretApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApiSecretId))
			.ReverseMap();

		CreateMap<ApiSecretDto, ApiSecretApiDto>(MemberList.Destination);
		CreateMap<ApiSecretsDto, ApiSecretsApiDto>(MemberList.Destination);

		// Api Properties
		CreateMap<ApiResourcePropertiesDto, ApiResourcePropertyApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApiResourcePropertyId))
			.ReverseMap();

		CreateMap<ApiResourcePropertyDto, ApiResourcePropertyApiDto>(MemberList.Destination);
		CreateMap<ApiResourcePropertiesDto, ApiResourcePropertiesApiDto>(MemberList.Destination);
	}
}

