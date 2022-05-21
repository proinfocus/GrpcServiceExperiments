using GrpcService.Server.Endpoints;
using GrpcService.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ProcessorService>();


////The following 2 lines should be commented out once the Endpoints are generated.
//Proinfocus.gRPC.CreateEndpoints<GreeterService>();
//Proinfocus.gRPC.CreateEndpoints<ProcessorService>();

string grpcUrl = app.Configuration.GetSection("gRPCUrl").Value;
app.UseGreeterServiceEndpoints(grpcUrl);
app.UseProcessorServiceEndpoints(grpcUrl);
app.Run();
