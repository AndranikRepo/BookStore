using AutoMapper;
using BookStore.Data.Entities;
using BookStore.Shared.Dto;

namespace BookStore.Presentation.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<Book, BookDto>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<BookCategory, CategoryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Category.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Category.Name));
            CreateMap<UpdateBookDto, Book>();
            CreateMap<UpdateCategoryDto, BookCategory>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(y => y.Id));
        }
    }
}
