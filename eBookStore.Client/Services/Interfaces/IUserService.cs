using eBookStore.Domains.Entities;
using eBookStore.Services.ViewModels.UserViewModels;

namespace eBookStore.Client.Services.Interfaces;
public interface IUserService
{
    Task<UserViewModel?> CreateUser(UserCreateModel model);
    Task<IEnumerable<UserViewModel>?> GetAllUserAsync(string search="");
    Task<UserViewModel?> GetUserById(Guid id);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> UpdateUserAsync(UserUpdateModel model);
}