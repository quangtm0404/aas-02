using eBookStore.Client.Models;
using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Newtonsoft.Json;


namespace eBookStore.Client.Services;
public class UserService : IUserService
{
    private readonly IBaseService _baseService;
    public UserService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<UserViewModel?> CreateUser(UserCreateModel model)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.POST,
            Data = model,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/users"
        });

        if (result is not null) return JsonConvert.DeserializeObject<UserViewModel?>(result);
        else return null;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.DELETE,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/users/{id}"
        });
        if (!string.IsNullOrEmpty(result))
            if (result == "NoContent") return true;
        return false;

    }

    public async Task<IEnumerable<UserViewModel>?> GetAllUserAsync()
    {
        var result = await _baseService.SendAsync(new RequestModel
        {
            APIType = StaticDetails.APIType.GET,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/users"
        });
        if (!string.IsNullOrEmpty(result))
        {
            System.Console.WriteLine("Result : " + result);
            var listUser =
            JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(result);
            return listUser;

        }
        else return new List<UserViewModel>();
    }

    public async Task<UserViewModel?> GetUserById(Guid id)
    {
        var resultString = await _baseService.SendAsync(new RequestModel
        {
            APIType = StaticDetails.APIType.GET,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/users/{id}"
        });
        if (!string.IsNullOrEmpty(resultString))
            return JsonConvert.DeserializeObject<UserViewModel>(resultString);
        else return null;
    }

    public async Task<bool> UpdateUserAsync(UserUpdateModel model)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.PUT,
            Data = model,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/users"
        });
        if (!string.IsNullOrEmpty(result))
            if (result == "NoContent") return true;
        return false;
    }
}