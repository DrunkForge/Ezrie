Import-Module .\etc\EzriePS\EzriePS.psm1
if ($true -ne (Test-Path -Path ".\$root.sln" -PathType Leaf)) 
{ 
	Write-Host "Please run this script from the directory that contains $root.sln" -ForegroundColor Red
	Exit
}

if (Test-Path "$location\.git" -IsValid) {
	Write-Host "The git repo already exists." -ForegroundColor Blue
} else {
	Write-Host "Creating the git repo." -ForegroundColor Blue
	git init -b main
	git add --all
	git commit -m "Initial Commit"
}	

if ($false -eq (Test-Path "$apps" -IsValid)) { New-Item -Path $location -Name "$apps" -ItemType "Directory" }
if ($false -eq (Test-Path "$gateways" -IsValid)) { New-Item -Path $location -Name "$gateways" -ItemType "Directory" }
if ($false -eq (Test-Path "$services" -IsValid)) { New-Item -Path $location -Name "$services" -ItemType "Directory" }
if ($false -eq (Test-Path "$shared" -IsValid)) { New-Item -Path $location -Name "$shared" -ItemType "Directory" }

Write-Host "Location:  $location" -ForegroundColor Blue
Write-Host "Apps:      $apps" -ForegroundColor Blue
Write-Host "Gateways:  $gateways" -ForegroundColor Blue
Write-Host "Services:  $services" -ForegroundColor Blue
Write-Host "Shared:    $shared" -ForegroundColor Blue

# New-App "CRM" "blazor"

# New-Solution

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

# Update-Solution

# dotnet new skoruba.is4admin --name "Ezrie.AccountManagement" --title "Ezrie Account" --adminemail "root@ezrie.ca" --adminpassword "nji(0okM" --adminrole "System Administrators" --adminclientid "ezrie_administration_service" --adminclientsecret "e2c06464-d26d-4518-b6e8-6b0c271d1da7" --dockersupport true
