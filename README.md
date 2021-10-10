```
dotnet publish -c Release -o Published
docker-compose -f docker-compose.yml up
dotnet Published\ElasticsearchDockerSample.dll
docker-compose -f docker-compose.yml down
```

Type `http://localhost:5601` to open Kibana.