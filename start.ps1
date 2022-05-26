Import-Module .\etc\EzriePS\EzriePS.psm1
if ($true -ne (Test-Path -Path ".\$root.sln" -IsValid -PathType Leaf)) 
{ 
	Write-Host "Please run this script from the directory that contains $root.sln" -ForegroundColor Red
	Exit
}

<# Check development certificates #>
<# Invoke-CreateCertificate #>

<# Check Docker containers #>
<#Start-Containers #>

<# Run all services #>
tye run --watch
