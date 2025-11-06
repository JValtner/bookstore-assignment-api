using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.DTO.ExternalComics;
using BookstoreApplication.Models;
using BookstoreApplication.Models.ExternalComics;

namespace BookstoreApplication.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- User and Profile Mappings ---
            CreateMap<RegistrationDto, ApplicationUser>();
            CreateMap<LoginDto, ApplicationUser>();
            CreateMap<ApplicationUser, ProfileDto>();

            // --- Book Mappings ---
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

            // --- Author, Publisher Mappings ---
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Publisher, PublisherDTO>().ReverseMap();

            // --- LocalIssue (External Comics) Mappings ---
            CreateMap<LocalIssueDTO, LocalIssue>()
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();



            // --- Nested Classes Mapping (optional but recommended) ---
            CreateMap<ComicVineVolume, ComicVineVolume>().ReverseMap();
            CreateMap<ComicVineImage, ComicVineImage>().ReverseMap();
        }
    }
}
