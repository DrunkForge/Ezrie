[CmdletBinding()]
param (
	[Parameter()]
	[System.String]
	$Migration = "CreateDatabase"
)

Import-Module .\etc\EzriePS\EzriePS.psm1
if ($true -ne (Test-Path -Path ".\$root.sln" -IsValid -PathType Leaf)) 
{ 
	Write-Host "Please run this script from the directory that contains $root.sln" -ForegroundColor Red
	Exit
}

# Reset-Containers

Add-Migration "$Migration" "AdministrationService" "services"
# Add-Migration "$Migration" "ClientManager" "services"
# Add-Migration "$Migration" "Crm" "apps"
# Add-Migration "$Migration" "Account" "apps"

Set-Location "$location\shared\src\$root.DbMigrator\"
dotnet run $dbMigrator
Set-Location $location
