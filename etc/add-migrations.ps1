param([string] $migration = 'DbInit', [string] $migrationProviderName = 'All', [string] $targetContext = 'All')
$projectName = "Ezrie.AccountManagement";
$currentPath = Get-Location
Set-Location "../apps/$projectName/src/$projectName.Web"
Copy-Item appsettings.json -Destination appsettings-backup.json
$settings = Get-Content appsettings.json -raw

#Initialze db context and define the target directory
$targetContexts = @{ 
    AdminIdentityDbContext                = "Migrations\Identity"
    AdminLogDbContext                     = "Migrations\Logging";
    IdentityServerConfigurationDbContext  = "Migrations\IdentityServerConfiguration";
    IdentityServerPersistedGrantDbContext = "Migrations\IdentityServerGrants";
    AdminAuditLogDbContext                = "Migrations\AuditLogging";
    IdentityServerDataProtectionDbContext = "Migrations\DataProtection";
}

#Initialize the db providers and it's respective projects
$dbProviders = @{
    SqlServer  = "..\$projectName.EntityFrameworkCore.SqlServer\$projectName.EntityFrameworkCore.SqlServer.csproj";
    PostgreSQL = "..\$projectName.EntityFrameworkCore.PostgreSQL\$projectName.EntityFrameworkCore.PostgreSQL.csproj";
    MySql      = "..\$projectName.EntityFrameworkCore.MySql\$projectName.EntityFrameworkCore.MySql.csproj";
}

#Fix issue when the tools is not installed and the nuget package does not work see https://github.com/MicrosoftDocs/azure-docs/issues/40048
Write-Host "Updating donet ef tools"
$env:Path += "	% USERPROFILE % \.dotnet\tools";
dotnet tool update --global dotnet-ef

Write-Host "Start migrate projects" -ForegroundColor Green
foreach ($provider in $dbProviders.Keys) {

    if ($migrationProviderName -eq 'All' -or $migrationProviderName -eq $provider) {
    
        $projectPath = (Get-Item -Path $dbProviders[$provider] -Verbose).FullName;
        Write-Host "Generate migration for db provider: $provider, for project path: $projectPath"  -ForegroundColor Green

        $providerName = '"ProviderType": "' + $provider + '"'

        $settings = $settings -replace '"ProviderType".*', $providerName
        $settings | set-content appsettings.json
        if ((Test-Path $projectPath) -eq $true) {
            foreach ($context in $targetContexts.Keys) {
                
                if ($targetContext -eq 'All' -or $context -eq $targetContext) {

                    $migrationPath = $targetContexts[$context];

                    Write-Host "Adding Migration: $migration for $provider using $context"  -ForegroundColor Green
                    dotnet ef migrations add $migration -c $context -o $migrationPath -p $projectPath
                }
            } 
        } else {
			Write-Host "Project path does not exist: $projectPath" -ForegroundColor Green
		}		        
    }
}

Remove-Item appsettings.json
Copy-Item appsettings-backup.json -Destination appsettings.json
Remove-Item appsettings-backup.json
Set-Location $currentPath
