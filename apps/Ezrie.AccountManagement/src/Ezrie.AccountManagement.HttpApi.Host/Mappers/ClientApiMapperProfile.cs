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
using Ezrie.AccountManagement.Dtos.Clients;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration;

namespace Ezrie.AccountManagement.Mappers;

public class ClientApiMapperProfile : Profile
{
	public ClientApiMapperProfile()
	{
		// Client
		CreateMap<ClientDto, ClientApiDto>(MemberList.Destination)
			.ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
			.ReverseMap();

		CreateMap<ClientsDto, ClientsApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ClientCloneApiDto, ClientCloneDto>(MemberList.Destination)
			.ReverseMap();

		// Client Secrets
		CreateMap<ClientSecretsDto, ClientSecretApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientSecretId))
			.ReverseMap();

		CreateMap<ClientSecretDto, ClientSecretApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ClientSecretsDto, ClientSecretsApiDto>(MemberList.Destination);

		// Client Properties
		CreateMap<ClientPropertiesDto, ClientPropertyApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientPropertyId))
			.ReverseMap();

		CreateMap<ClientPropertyDto, ClientPropertyApiDto>(MemberList.Destination)
			.ReverseMap();

		CreateMap<ClientPropertiesDto, ClientPropertiesApiDto>(MemberList.Destination);

		// Client Claims
		CreateMap<ClientClaimsDto, ClientClaimApiDto>(MemberList.Destination)
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientClaimId))
			.ReverseMap();

		CreateMap<ClientClaimDto, ClientClaimApiDto>(MemberList.Destination)
			.ReverseMap();
		CreateMap<ClientClaimsDto, ClientClaimsApiDto>(MemberList.Destination);
	}
}

