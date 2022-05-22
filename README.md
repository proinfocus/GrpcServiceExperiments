# gRPC Service Experiments

An experiment to generate minimal api endpoints for the gRPC services.

### How does it work?
- Create a gRPC Service web project.
- Add NuGet reference from https://www.nuget.org/packages/Proinfocus.gRPC/
- After mapping gRPC service like ```app.MapGrpcService<GreeterService>();``` in ```program.cs``` add the following line for each service for eg: ```Proinfocus.gRPC.CreateEndpoints<GreeterService>();```. **Note: this line should be removed/commented out once it is run as it will generate the endpoints in the ```Endpoints``` folder for the given service.**
- Define the gRPC Url for eg: ```string grpcUrl = "https://localhost:7654";```
- Finally use each one of the created gRPC service endpoints for eg: ```app.UseGreeterServiceEndpoints(grpcUrl);```
