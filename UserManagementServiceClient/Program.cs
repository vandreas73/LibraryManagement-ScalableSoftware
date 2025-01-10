// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using UserManagementServiceClient;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("https://localhost:7275");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine($"Greeting {reply.Message}");