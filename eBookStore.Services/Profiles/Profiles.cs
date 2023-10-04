using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.ViewModels.AuthorViewModels;
using eBookStore.Services.ViewModels.BookAuthorModels;
using eBookStore.Services.ViewModels.BookModels;
using eBookStore.Services.ViewModels.PublisherModels;
using eBookStore.Services.ViewModels.RoleViewModels;
using eBookStore.Services.ViewModels.UserViewModels;

namespace eBookStore.Services.Profiles;
public class Profiles : Profile 
{
    public Profiles()
    {
        CreateMap<RoleViewModel, Role>().ReverseMap();
        CreateMap<RoleCreateModel, Role>().ReverseMap();

        // UserMappingProfiles
        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();

        // Author Mapping Profiles
        CreateMap<AuthorViewModel, Author>().ReverseMap();
        CreateMap<AuthorCreateModel, Author>().ReverseMap();
        CreateMap<AuthorUpdateModel, Author>().ReverseMap();
        CreateMap<PublisherViewModel, Publisher>().ReverseMap();
        CreateMap<PublisherUpdateModel, Publisher>().ReverseMap();
        CreateMap<PublisherCreateModel, Publisher>().ReverseMap();

        CreateMap<BookViewModel, Book>().ReverseMap();
        CreateMap<BookCreateModel, Book>().ReverseMap();
        CreateMap<BookUpdateModel, Book>().ReverseMap();;
        CreateMap<BookAuthorViewModel, BookAuthor>().ReverseMap();
        CreateMap<BookAuthorUpdateModel, BookAuthor>().ReverseMap();
        CreateMap<BookAuthorCreateModel, BookAuthor>().ReverseMap();

    }
}