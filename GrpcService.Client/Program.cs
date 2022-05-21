#nullable disable

using Grpc.Net.Client;
using GrpcService.Server;

var channel = GrpcChannel.ForAddress("https://localhost:7114");
var client = new Greeter.GreeterClient(channel);
var result = await client.SayHelloAsync(new HelloRequest { Name = "Rahul" });
Console.WriteLine($"Reply from Greeter Service: {result.Message}!");


var newClient = new Processor.ProcessorClient(channel);
var newResult = await newClient.ProcessMessageAsync(new Request { Name = "Rahul" });
Console.WriteLine($"Reply from Processor Service: {result.Message}!");

