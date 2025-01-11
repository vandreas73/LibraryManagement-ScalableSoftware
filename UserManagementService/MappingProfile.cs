using AutoMapper;
using UserManagementService.Models;

namespace UserManagementService
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserModel, UserDTO>();
			CreateMap<UserDTO, UserModel>();
		}
	}
}
