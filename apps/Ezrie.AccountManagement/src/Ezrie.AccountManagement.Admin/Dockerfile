#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Admin/Ezrie.AccountManagement.Admin.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Admin/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain.Shared/Ezrie.AccountManagement.Domain.Shared.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain.Shared/"]
COPY ["shared/src/Ezrie.Logging/Ezrie.Logging.csproj", "shared/src/Ezrie.Logging/"]
COPY ["shared/src/Ezrie.Domain/Ezrie.Domain.csproj", "shared/src/Ezrie.Domain/"]
COPY ["shared/src/Ezrie.Domain.Shared/Ezrie.Domain.Shared.csproj", "shared/src/Ezrie.Domain.Shared/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.MySql/Ezrie.AccountManagement.EntityFrameworkCore.MySql.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.MySql/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore/Ezrie.AccountManagement.EntityFrameworkCore.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain/Ezrie.AccountManagement.Domain.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Domain/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.SqlServer/Ezrie.AccountManagement.EntityFrameworkCore.SqlServer.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.SqlServer/"]
COPY ["apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL/Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL.csproj", "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.EntityFrameworkCore.PostgreSQL/"]
RUN dotnet restore "apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Admin/Ezrie.AccountManagement.Admin.csproj"
COPY . .
WORKDIR "/src/apps/Ezrie.AccountManagement/src/Ezrie.AccountManagement.Admin"
RUN dotnet build "Ezrie.AccountManagement.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.AccountManagement.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ezrie.AccountManagement.Admin.dll"]