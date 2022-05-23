using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcService.Server.Services;

public class ProcessorService : Processor.ProcessorBase
{
    public override Task<Response> ProcessMessage(Request request, ServerCallContext context)
    {
        return Task.FromResult(new Response
        {
            Message = "Processed: " + request.Name
        });
    }

    public override Task<Response> DisplayCurrentDateTime(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new Response
        {
            Message = DateTime.UtcNow.ToString()
        });
    }
}
