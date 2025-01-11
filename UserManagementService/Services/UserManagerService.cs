using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using UserManagementService;
using UserManagementService.Models;

namespace UserManagementService.Services
{
	public class UserManagerService : UserManager.UserManagerBase
	{
		private readonly ILogger<UserManagerService> _logger;
		private readonly UserContext userContext;
		private readonly IMapper mapper;

		public UserManagerService(ILogger<UserManagerService> logger, UserContext userContext, IMapper mapper)
		{
			_logger = logger;
			this.userContext = userContext;
			this.mapper = mapper;
		}

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			return Task.FromResult(new HelloReply
			{
				Message = "Hello " + request.Name
			});
		}

		public override async Task<UserDTO> Get(UserIdRequest request, ServerCallContext context)
		{
			var user = await userContext.Users.FindAsync(request.Id);
			if (user == null)
			{
					return new UserDTO();
			}
			return mapper.Map<UserDTO>(user);
		}

		public override async Task GetAll(Empty request, IServerStreamWriter<UserDTO> responseStream, ServerCallContext context)
		{
			var users = await userContext.Users.ToListAsync();
			foreach (var user in users)
			{
				await responseStream.WriteAsync(mapper.Map<UserDTO>(user));
			}
		}

		public override async Task<UserDTO> Create(UserDTO request, ServerCallContext context)
		{
			if (request.Id != 0)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Id should not be provided"));
			}
			var user = mapper.Map<UserModel>(request);
			userContext.Users.Add(user);
			await userContext.SaveChangesAsync();
			return mapper.Map<UserDTO>(user);
		}

		public override async Task<UserDTO> Update(UserDTO request, ServerCallContext context)
		{
			var user = mapper.Map<UserModel>(request);
			if (!userContext.Users.Any(u => u.Id == user.Id))
			{
				throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
			}
			userContext.Users.Update(user);
			await userContext.SaveChangesAsync();
			return mapper.Map<UserDTO>(user);
		}

		public override async Task<Empty> Delete(UserIdRequest request, ServerCallContext context)
		{
			var user = await userContext.Users.FindAsync(request.Id);
			if (user == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
			}
			userContext.Users.Remove(user);
			await userContext.SaveChangesAsync();
			return new Empty();
		}

		public override async Task Search(UserDTO request, IServerStreamWriter<UserDTO> responseStream, ServerCallContext context)
		{
			var users = await userContext.Users
				.Where(u => u.Name.Contains(request.Name) && u.Address.Contains(request.Address) 
				&& u.Email.Contains(request.Email)).ToListAsync();
			foreach (var user in users)
			{
				await responseStream.WriteAsync(mapper.Map<UserDTO>(user));
			}
		}
	}
}
