using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Models;
namespace BookstoreApplication.Settings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, ApplicationUser>();
            CreateMap<LoginDto, ApplicationUser>();
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.BookAge,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year))
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty))
                .ReverseMap();
            CreateMap<Book, BookDetailsDTO>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author != null ? src.Author.FullName : string.Empty))
                .ForMember(dest => dest.PublisherName,
                    opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Name : string.Empty))
                .ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
        }

    }
}
