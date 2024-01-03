docker rm -f frontend
docker rm -f dotnet

docker rmi store-frontend
docker rmi store-dotnet

docker-compose -f docker-compose.services.yml up -d
docker-compose -f docker-compose.apps.yml up -d
