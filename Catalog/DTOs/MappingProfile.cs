using Catalog.Models;
using AutoMapper;

namespace Catalog.DTOs
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Book, BookDTO>()
				.ForMember(dest => dest.AuthorName, opt =>opt.MapFrom(src => src.Author.Name));
			CreateMap<BookDTO, Book>();
			CreateMap<Author, AuthorDTO>();
			CreateMap<AuthorDTO, Author>();
		}

	}
}
