Import-Module .\etc\EzriePS\EzriePS.psm1
if ($true -ne (Test-Path -Path ".\$root.sln" -IsValid -PathType Leaf)) 
{ 
	Write-Host "Please run this script from the directory that contains $root.sln" -ForegroundColor Red
	Exit
}

docker stop ezrie_postgres ezrie_redis ezrie_rabbitmq
docker rm ezrie_postgres ezrie_redis ezrie_rabbitmq
docker volume rm docker_ezrie_postgres_data
docker-compose -f docker-compose-infrastructure.yml up -d 

tye run --watch
