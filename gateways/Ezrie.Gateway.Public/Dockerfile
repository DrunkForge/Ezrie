#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gateways/Ezrie.Gateway.Public/Ezrie.Gateway.Public.csproj", "gateways/Ezrie.Gateway.Public/"]
COPY ["shared/src/Ezrie.Logging/Ezrie.Logging.csproj", "shared/src/Ezrie.Logging/"]
COPY ["shared/src/Ezrie.Core/Ezrie.Core.csproj", "shared/src/Ezrie.Core/"]
COPY ["shared/src/Ezrie.Hosting.Microservices/Ezrie.Hosting.Microservices.csproj", "shared/src/Ezrie.Hosting.Microservices/"]
COPY ["shared/src/Ezrie.Hosting.AspNetCore/Ezrie.Hosting.AspNetCore.csproj", "shared/src/Ezrie.Hosting.AspNetCore/"]
COPY ["shared/src/Ezrie.Hosting/Ezrie.Hosting.csproj", "shared/src/Ezrie.Hosting/"]
COPY ["shared/src/Ezrie.Domain/Ezrie.Domain.csproj", "shared/src/Ezrie.Domain/"]
COPY ["shared/src/Ezrie.Domain.Shared/Ezrie.Domain.Shared.csproj", "shared/src/Ezrie.Domain.Shared/"]
COPY ["shared/src/Ezrie.Hosting.Gateways/Ezrie.Hosting.Gateways.csproj", "shared/src/Ezrie.Hosting.Gateways/"]
RUN dotnet restore "gateways/Ezrie.Gateway.Public/Ezrie.Gateway.Public.csproj"
COPY . .
WORKDIR "/src/gateways/Ezrie.Gateway.Public"
RUN dotnet build "Ezrie.Gateway.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.Gateway.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ezrie.Gateway.Public.dll"]