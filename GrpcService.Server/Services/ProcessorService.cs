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
}
