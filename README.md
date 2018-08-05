# uklon-testapp
This project is a test assignment for the position Senior .Net Developer for the Uklon company. 
The project implemented with Web API .Net Core and Entity Framework. To compile and run you need the .Net Core 2.0 installed on your PC.

To run application you need:
1. Checkout the project to some directory with 'git clone'
2. In this directory run command 'dotnet run'

Application can be configured with two options:
* Yandex traffic provider that returns traffic data from Yandex API.
* Stub traffic provider that returns randomly generated traffic data.

To config application with Yandex traffic provider with the cache in the appsettings.json file set option "TrafficProvider": "Yandex".
To config application to use stub provider without caching in the appsettings.json file set option "TrafficProvider": "Stub".

To use application from Ukraint you need also to specify proxy in the config file. By default "Proxy": "http://88.99.149.188:31288"

Application API supports 3 endpoints:

### 1. Return all regions.
GET /api/regions/all
```text
[
  {
    "code": <LONG>, // code of region
    "name": <STRING> // name of region
  },
  ...
]
```

### 2. Return traffic data for specified region.
GET /api/traffic/{regionCode}, where {regionCode} - code of some region
```text
{
  "traffic":
   {
      "level": <LONG>, // traffic level code
      "hint": <STRING> // user friendly traffic level
   },
   "region":
   {
      "code": <LONG>, // code of region
      "name": <STRING> // name of region
    }
}
```
Server can return 404 if the region code doesn't exist. Traffic can be null if data for specified region is absent.

### 3. Return traffic data for all regions.
GET /api/traffic/all
``` 
Response contains array of object that was specified in the previous endpoint
```
