#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/Ezrie.IdentityService/NuGet.Config", "services/Ezrie.IdentityService/"]
COPY ["services/Ezrie.IdentityService/host/Ezrie.IdentityService.HttpApi.Host/Ezrie.IdentityService.HttpApi.Host.csproj", "services/Ezrie.IdentityService/host/Ezrie.IdentityService.HttpApi.Host/"]
COPY ["shared/src/Ezrie.Logging/Ezrie.Logging.csproj", "shared/src/Ezrie.Logging/"]
COPY ["shared/src/Ezrie.Core/Ezrie.Core.csproj", "shared/src/Ezrie.Core/"]
COPY ["services/Ezrie.TenantService/src/Ezrie.TenantService.EntityFrameworkCore/Ezrie.TenantService.EntityFrameworkCore.csproj", "services/Ezrie.TenantService/src/Ezrie.TenantService.EntityFrameworkCore/"]
COPY ["services/Ezrie.TenantService/src/Ezrie.TenantService.Domain/Ezrie.TenantService.Domain.csproj", "services/Ezrie.TenantService/src/Ezrie.TenantService.Domain/"]
COPY ["shared/src/Ezrie.Domain/Ezrie.Domain.csproj", "shared/src/Ezrie.Domain/"]
COPY ["shared/src/Ezrie.Domain.Shared/Ezrie.Domain.Shared.csproj", "shared/src/Ezrie.Domain.Shared/"]
COPY ["services/Ezrie.TenantService/src/Ezrie.TenantService.Domain.Shared/Ezrie.TenantService.Domain.Shared.csproj", "services/Ezrie.TenantService/src/Ezrie.TenantService.Domain.Shared/"]
COPY ["shared/src/Ezrie.EntityFrameworkCore.Migrations/Ezrie.EntityFrameworkCore.Migrations.csproj", "shared/src/Ezrie.EntityFrameworkCore.Migrations/"]
COPY ["shared/src/Ezrie.EntityFrameworkCore/Ezrie.EntityFrameworkCore.csproj", "shared/src/Ezrie.EntityFrameworkCore/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.EntityFrameworkCore.Migrations/Ezrie.IdentityService.EntityFrameworkCore.Migrations.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.EntityFrameworkCore.Migrations/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.EntityFrameworkCore/Ezrie.IdentityService.EntityFrameworkCore.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.EntityFrameworkCore/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.Domain/Ezrie.IdentityService.Domain.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.Domain/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.Domain.Shared/Ezrie.IdentityService.Domain.Shared.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.Domain.Shared/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.Application.Contracts/Ezrie.IdentityService.Application.Contracts.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.Application.Contracts/"]
COPY ["services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.EntityFrameworkCore/Ezrie.AdministrationService.EntityFrameworkCore.csproj", "services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.EntityFrameworkCore/"]
COPY ["services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.Domain/Ezrie.AdministrationService.Domain.csproj", "services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.Domain/"]
COPY ["services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.Domain.Shared/Ezrie.AdministrationService.Domain.Shared.csproj", "services/Ezrie.AdministrationService/src/Ezrie.AdministrationService.Domain.Shared/"]
COPY ["shared/src/Ezrie.Hosting.Microservices/Ezrie.Hosting.Microservices.csproj", "shared/src/Ezrie.Hosting.Microservices/"]
COPY ["shared/src/Ezrie.Hosting.AspNetCore/Ezrie.Hosting.AspNetCore.csproj", "shared/src/Ezrie.Hosting.AspNetCore/"]
COPY ["shared/src/Ezrie.Hosting/Ezrie.Hosting.csproj", "shared/src/Ezrie.Hosting/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.HttpApi/Ezrie.IdentityService.HttpApi.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.HttpApi/"]
COPY ["services/Ezrie.IdentityService/src/Ezrie.IdentityService.Application/Ezrie.IdentityService.Application.csproj", "services/Ezrie.IdentityService/src/Ezrie.IdentityService.Application/"]
RUN dotnet restore "services/Ezrie.IdentityService/host/Ezrie.IdentityService.HttpApi.Host/Ezrie.IdentityService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/Ezrie.IdentityService/host/Ezrie.IdentityService.HttpApi.Host"
RUN dotnet build "Ezrie.IdentityService.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.IdentityService.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ezrie.IdentityService.HttpApi.Host.dll"]