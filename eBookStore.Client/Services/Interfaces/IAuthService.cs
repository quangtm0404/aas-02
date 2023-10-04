using eBookStore.Services.ViewModels.UserViewModels;

namespace eBookStore.Client.Services.Interfaces;
public interface IAuthService
{
    Task<LoginResponseModel?> LoginAsync(LoginModel model);
}