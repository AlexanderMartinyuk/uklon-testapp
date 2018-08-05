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
