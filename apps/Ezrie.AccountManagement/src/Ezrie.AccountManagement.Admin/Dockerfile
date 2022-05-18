FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ezrie.AccountManagement.Admin/Ezrie.AccountManagement.Admin.csproj", "src/Ezrie.AccountManagement.Admin/"]
COPY ["src/Ezrie.AccountManagement.Admin.EntityFramework.Shared/Ezrie.AccountManagement.Admin.EntityFramework.Shared.csproj", "src/Ezrie.AccountManagement.Admin.EntityFramework.Shared/"]
COPY ["src/Ezrie.AccountManagement.Admin.EntityFramework.SqlServer/Ezrie.AccountManagement.Admin.EntityFramework.SqlServer.csproj", "src/Ezrie.AccountManagement.Admin.EntityFramework.SqlServer/"]
COPY ["src/Ezrie.AccountManagement.Domain.Shared/Ezrie.AccountManagement.Domain.Shared.csproj", "src/Ezrie.AccountManagement.Domain.Shared/"]
COPY ["src/Ezrie.AccountManagement.Admin.EntityFramework.PostgreSQL/Ezrie.AccountManagement.Admin.EntityFramework.PostgreSQL.csproj", "src/Ezrie.AccountManagement.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Ezrie.AccountManagement.Admin.EntityFramework.MySql/Ezrie.AccountManagement.Admin.EntityFramework.MySql.csproj", "src/Ezrie.AccountManagement.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/Ezrie.AccountManagement.Admin/Ezrie.AccountManagement.Admin.csproj"
COPY . .
WORKDIR "/src/src/Ezrie.AccountManagement.Admin"
RUN dotnet build "Ezrie.AccountManagement.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.AccountManagement.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Ezrie.AccountManagement.Admin.dll"]
