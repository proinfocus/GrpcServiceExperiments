using Grpc.Net.Client;
using System.Text.Json;

namespace GrpcService.Server.Endpoints;

public static class GreeterServiceEndpoints
{
	public static void UseGreeterServiceEndpoints(this WebApplication app, string grpcUrl)
    {
        var channel = GrpcChannel.ForAddress(grpcUrl);
        app.MapPost("/grpc/Greeter/SayHello", async (HelloRequest request) =>
        {
            try
            {
                var result = await new Greeter.GreeterClient(channel).SayHelloAsync(request);
                return Results.Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex) { return Results.Problem(ex.Message); }
        });

        app.MapPost("/grpc/Greeter/SayBye", async (HelloRequest request) =>
        {
            try
            {
                var result = await new Greeter.GreeterClient(channel).SayByeAsync(request);
                return Results.Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex) { return Results.Problem(ex.Message); }
        });


    }
}