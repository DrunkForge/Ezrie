#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.STS/Ezrie.AccountManagement.STS.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.STS/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain.Shared/Ezrie.AccountManagement.Domain.Shared.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain.Shared/"]
COPY ["shared/src/Ezrie.Domain.Shared/Ezrie.Domain.Shared.csproj", "shared/src/Ezrie.Domain.Shared/"]
COPY ["shared/src/Ezrie.Core/Ezrie.Core.csproj", "shared/src/Ezrie.Core/"]
COPY ["shared/src/Ezrie.Logging/Ezrie.Logging.csproj", "shared/src/Ezrie.Logging/"]
COPY ["shared/src/Ezrie.Hosting.AspNetCore/Ezrie.Hosting.AspNetCore.csproj", "shared/src/Ezrie.Hosting.AspNetCore/"]
COPY ["shared/src/Ezrie.Hosting/Ezrie.Hosting.csproj", "shared/src/Ezrie.Hosting/"]
COPY ["shared/src/Ezrie.Domain/Ezrie.Domain.csproj", "shared/src/Ezrie.Domain/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore/Ezrie.AccountManagement.EntityFrameworkCore.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain/Ezrie.AccountManagement.Domain.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain/"]
RUN dotnet restore "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.STS/Ezrie.AccountManagement.STS.csproj"
COPY . .
WORKDIR "/src/apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.STS"
RUN dotnet build "Ezrie.AccountManagement.STS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.AccountManagement.STS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ezrie.AccountManagement.STS.dll"]