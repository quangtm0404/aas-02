using eBookStore.Services.ViewModels.UserViewModels;

namespace eBookStore.Services.Services.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<UserViewModel> LoginAsync(string email, string password);
        Task<UserViewModel> CreateAsync(UserCreateModel userCreateModel);
        Task<bool> DeleteAsync(Guid id); 
        Task<UserViewModel> UpdateAsync(UserUpdateModel userUpdateModel);
    }
}