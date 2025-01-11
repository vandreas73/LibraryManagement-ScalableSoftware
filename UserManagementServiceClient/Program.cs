// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using System.Net.Sockets;
using UserManagementService;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("https://localhost:7275");
var client = new UserManager.UserManagerClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine($"Greeting {reply.Message}");

var getReply = await client.GetAsync(new UserIdRequest { Id = 2});
Console.WriteLine($"User {getReply}");


//var addReply = await client.CreateAsync(new UserDTO { Name = "Cecil", Email = "c@c.com", Address = "2345 Alcsútdoboz, Ady Endre u. 1." });

//var updateReply = await client.UpdateAsync(new UserDTO { Id = 3, Name = "Cecil", Email = "c@updated.com", Address = "2345 Alcsútdoboz, Ady Endre u. 1." });

//var deleteReply = await client.DeleteAsync(new UserIdRequest { Id = 3 });


var getAllReply = client.GetAll(new Google.Protobuf.WellKnownTypes.Empty());
await foreach (var user in getAllReply.ResponseStream.ReadAllAsync())
{
	Console.WriteLine($"User {user}");
}

var searchReply = client.Search(new UserDTO { Name = "Cecil" });
await foreach (var user in searchReply.ResponseStream.ReadAllAsync())
{
	Console.WriteLine($"User {user}");
}