using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.ViewModels.AuthorViewModels;
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

        
    }
}