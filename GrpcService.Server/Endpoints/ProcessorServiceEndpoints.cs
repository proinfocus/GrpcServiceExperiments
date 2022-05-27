using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System.Text.Json;

namespace GrpcService.Server.Endpoints;

public static class ProcessorServiceEndpoints
{
    public static void UseProcessorServiceEndpoints(this WebApplication app, string grpcUrl)
    {
        var channel = GrpcChannel.ForAddress(grpcUrl);
        app.MapPost("/grpc/Processor/ProcessMessage", async (Request request) =>
        {
            try
            {
                if (channel.State != Grpc.Core.ConnectivityState.Ready)
                    await channel.ConnectAsync();
                var result = await new Processor.ProcessorClient(channel).ProcessMessageAsync(request);
                return Results.Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex) { return Results.Problem(ex.Message); }
            finally { await channel.ShutdownAsync(); }
        });

        app.MapGet("/grpc/Processor/DisplayCurrentDateTime", async () =>
        {
            try
            {
                if (channel.State != Grpc.Core.ConnectivityState.Ready)
                    await channel.ConnectAsync();
                var result = await new Processor.ProcessorClient(channel).DisplayCurrentDateTimeAsync(new Empty());
                return Results.Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex) { return Results.Problem(ex.Message); }
            finally { await channel.ShutdownAsync(); }
        });


    }
}
