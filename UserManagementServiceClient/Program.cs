// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using System.Net.Sockets;
using UserManagementService;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("http://localhost:32770");
var client = new UserManager.UserManagerClient(channel);

var getReply = await client.GetAsync(new UserIdRequest { Id = 2});
Console.WriteLine($"User {getReply}");


//var addReply = client.Create(new UserDTO { Name = "Dénes", Email = "d@d.com", Address = "2345 Alcsútdoboz, Ady Endre u. 1." });

//var updateReply = await client.UpdateAsync(new UserDTO { Id = 8, Name = "Cecil", Email = "c@updated.com", Address = "2345 Alcsútdoboz, Ady Endre u. 1." });

//var deleteReply = await client.DeleteAsync(new UserIdRequest { Id = 7 });


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