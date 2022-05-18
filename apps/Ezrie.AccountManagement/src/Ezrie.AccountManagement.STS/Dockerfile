FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ezrie.AccountManagement.STS/Ezrie.AccountManagement.STS.csproj", "src/Ezrie.AccountManagement.STS/"]
COPY ["src/Ezrie.AccountManagement.Admin.EntityFramework.Shared/Ezrie.AccountManagement.Admin.EntityFramework.Shared.csproj", "src/Ezrie.AccountManagement.Admin.EntityFramework.Shared/"]
COPY ["src/Ezrie.AccountManagement.Domain.Shared/Ezrie.AccountManagement.Domain.Shared.csproj", "src/Ezrie.AccountManagement.Domain.Shared/"]
RUN dotnet restore "src/Ezrie.AccountManagement.STS/Ezrie.AccountManagement.STS.csproj"
COPY . .
WORKDIR "/src/src/Ezrie.AccountManagement.STS"
RUN dotnet build "Ezrie.AccountManagement.STS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ezrie.AccountManagement.STS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Ezrie.AccountManagement.STS.dll"]
