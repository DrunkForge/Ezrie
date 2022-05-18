# apps
# |- $root.Account
# |- $root.CRM
# |- $root.Website
# gateways
# services
# |- $root.CalendarService
# |- $root. ...
# shared
# |- $root.Common
# |- $root.DbMigrator
# |- $root.EntityFrameworkCore
# |- $root.Hosting
# |- $root.Hosting.AspNetCore
# |- $root.Hosting.AspnNetCore.Gateways
# |- $root.Hosting.AspNetCore.Microservices
# |- $root.Localization
# |- $root.Logging
# |- $root.Testing

Import-Module .\EzriePS\EzriePS.psm1
if ($true -ne (Test-Path -Path ".\$root.sln" -IsValid -PathType Leaf)) 
{ 
	Write-Host "Please run this script from the directory that contains $root.sln" -ForegroundColor Red
	Exit
}

if (Test-Path "$location\.git" -IsValid) {
	throw "There is already a git repo. Please remove it before running this script."
}

if (Test-Path "$apps" -IsValid) { New-Item -Path $location -Name "$apps" -ItemType "Directory" }
if (Test-Path "$services" -IsValid) { New-Item -Path $location -Name "$services" -ItemType "Directory" }
if (Test-Path "$shared" -IsValid) { New-Item -Path $location -Name "$shared" -ItemType "Directory" }

Write-Host "Location:     $location" -ForegroundColor Yellow
Write-Host "Applications: $apps" -ForegroundColor Yellow
Write-Host "Services:     $services" -ForegroundColor Yellow
Write-Host "Shared:       $shared" -ForegroundColor Yellow

git init -b main
git add --all
git commit -m "Initial Commit"

New-App "CRM" "blazor"

New-Solution

# New-Service "AdministrationService"
# Add-Module "AdministrationService" "Volo.AuditLogging"
# Add-Module "AdministrationService" "Volo.FeatureManagement"
# Add-Module "AdministrationService" "Volo.PermissionManagement"
# Add-Module "AdministrationService" "Volo.SettingManagement"
# New-Service "IdentityService"
# Add-Module "IdentityService" "Volo.Identity"
# Add-Module "IdentityService" "Volo.IdentityServer"
# New-Service "TenantService"
# Add-Module "TenantService" "Volo.TenantManagement"

Update-Solution

dotnet new skoruba.is4admin --name "Ezrie.AccountManagement" --title "Ezrie Account" --adminemail "root@ezrie.ca" --adminpassword "nji(0okM" --adminrole "System Administrators" --adminclientid "ezrie_administration_service" --adminclientsecret "e2c06464-d26d-4518-b6e8-6b0c271d1da7" --dockersupport true
