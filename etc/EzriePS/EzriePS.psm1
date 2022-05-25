$location = Get-Location
$root = "Ezrie"
$apps = "$location\apps"
$gateways = "$location\gateways"
$services = "$location\services"
$shared = "$location\shared"
$docker = "$location\etc\docker"
Export-ModuleMember -Variable location,root,apps,gateways,services,shared

if ($null -eq $location) {
	throw "The EzriePS module was not initialized correctly."
}


$cert = "$location\etc\dev-cert"
$dbMigrator = "$location\$shared\src\$root.DbMigrator\$root.DbMigrator.csproj"
Export-ModuleMember -Variable sln

Write-Host "Location:  $location" -ForegroundColor Yellow
Write-Host "Root:      $root" -ForegroundColor Yellow
Write-Host "Migrator:  $dbMigrator" -ForegroundColor Yellow

$requiredServices = @(
	'ezrie_postgres',
	'ezrie_rabbitmq',
	'ezrie_redis'
)
Export-ModuleMember -Variable requiredServices

function Start-Containers() {
	docker network create ezrie-crm-network
	docker-compose -f "$docker\docker-compose.infrastructure.yml" -f "$docker\docker-compose.infrastructure.override.yml" up -d
}
Export-ModuleMember -Function Start-Containers

function Stop-Containers() {
	docker-compose -f "$docker\docker-compose.infrastructure.yml" -f "$docker\docker-compose.infrastructure.override.yml" up -d
	docker network rm ezrie-crm-network
}
Export-ModuleMember -Function Stop-Containers

function Reset-Containers() {
	foreach ($requiredService in $requiredServices) {	
		if ( Get-ContainersStatus $requiredService ) {
			Write-Host "$requiredService [stopping]"
			docker stop $requiredService
			docker rm $requiredService
		}
	}

	docker volume rm "docker_ezrie_postgres_data"

	Start-Containers
}
Export-ModuleMember -Function Reset-Containers

function Get-ContainersStatus($name) {
	$nameParam = -join ("name=", $name)
	$serviceRunningStatus = docker ps --filter $nameParam
	$isDockerImageUp = $serviceRunningStatus -split " " -contains $name
	return $isDockerImageUp
}

function Remove-Unwanted($path) {
	Write-Host "Remove Unwanted ========================================================================" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/database) -ErrorAction SilentlyContinue
	Write-Host "Removed: $path/**/database" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/*.Host.Shared) -ErrorAction SilentlyContinue 
	Write-Host "Removed: $path/**/*.Host.Shared" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/*.IdentityServer) -ErrorAction SilentlyContinue 
	Write-Host "Removed: $path/**/*.IdentityServer" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/*.Installer) -ErrorAction SilentlyContinue 
	Write-Host "Removed: $path/**/*.Installer" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/*.MongoDB.Tests) -ErrorAction SilentlyContinue 
	Write-Host "Removed: $path/**/*.MongoDB.Tests" -ForegroundColor Yellow
	Remove-Item -Recurse -Force (Get-ChildItem -r $path/**/*.MongoDB) -ErrorAction SilentlyContinue 
	Write-Host "Removed: $path/**/*.MongoDB" -ForegroundColor Yellow
}

function New-App($app, $ui) {
	Write-Host "$root.$app **************************************************************************" -ForegroundColor Yellow
	abp new "$root.$app" -t app -u $ui -d ef -dbms PostgreSQL -m none --separate-identity-server -csf -o "$apps"
	Remove-Unwanted "$apps\$root.$app"
	git add --all
	git commit -m "$root.$app"
}
Export-ModuleMember -Function New-App

function New-Service($service) {
	Write-Host "New Service $root.$service ===================================================================" -ForegroundColor Yellow
	abp new "$root.$service" -t module -d ef -dbms PostgreSQL -csf -o "$services"
	Remove-Unwanted "$services\$root.$service"
	git add --all "$services\$root.$service"
	git commit -m "New Module $root.$service"
}
Export-ModuleMember -Function New-Service

function Add-Module($service, $module) {
	Write-Host "Add Module $module to $root.$service ===================================================================" -ForegroundColor Yellow
	abp add-module "$module" -s "$services\$root.$service\$root.$service.sln" --skip-db-migrations
	Remove-Unwanted "$services\$root.$service"
	git add --all
	git commit -m "Add Module $module to $root.$service"
}
Export-ModuleMember -Function Add-Module

function New-Solution() {
	Write-Host "$root.sln ==============================================================================" -ForegroundColor Yellow
	Set-Location $location
	dotnet new sln -n "$root" --force
	git add "$location\$root.sln"
	git commit -m "Created $root.sln"

	Update-Solution
}
Export-ModuleMember -Function New-Solution

function Update-Solution() {
	Set-Location $location
	dotnet sln add (Get-ChildItem -r **/*.csproj)
	git add --all
	git commit -m "Add projects to solution $root.sln"
}
Export-ModuleMember -Function Update-Solution

function Add-Migration($migration, $name, $base) {
	$Path = "$location\$base\$root.$name\src\$root.$name.EntityFrameworkCore.Migrations"
	$Context = $name + "MigrationsDbContext"
	$CsProj = "$Path\$root.$name.EntityFrameworkCore.Migrations.csproj"

	if ("CreateDatabase" -eq $migration){
		Remove-Item -Recurse -Force "$Path\Migrations" -ErrorAction SilentlyContinue
	}
	Set-Location -Path $Path
	Write-Host "Executing: dotnet ef migrations add $migration --context $Context --project $CsProj --startup-project $dbMigrator" -ForegroundColor Yellow
	dotnet ef migrations add "$migration" --context "$Context" --project "$CsProj" --startup-project "$dbMigrator"
	Set-Location $location
}
Export-ModuleMember -Function Add-Migration

function Invoke-MigrateDatabases() {
	Set-Location "$location\shared\src\$root.DbMigrator\"
	Write-Host "Executing: dotnet run $dbMigrator" -ForegroundColor Yellow
	dotnet run $dbMigrator
	Set-Location $location
}
Export-ModuleMember -Function Invoke-MigrateDatabases

function Invoke-CreateCertificate {
	if (Test-Path ".\localhost.pfx" -IsValid -PathType Leaf ) {
		Write-Information "Creating dev certificates..."
		Set-Location $cert
		dotnet dev-certs https --trust --export-path "localhost.pfx" --password "8b6039b6-c67a-448b-977b-0ce6d3fcfd49"
		Set-Location $location
	}
}
Export-ModuleMember -Function Invoke-CreateCertificate
