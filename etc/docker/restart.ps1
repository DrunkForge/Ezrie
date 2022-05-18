docker stop postgres redis rabbitmq
docker rm postgres redis rabbitmq
docker-compose -f docker-compose.infrastructure.yml -f docker-compose.infrastructure.override.yml up -d
