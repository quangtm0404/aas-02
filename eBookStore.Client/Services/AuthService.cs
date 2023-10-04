using eBookStore.Client.Models;
using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Newtonsoft.Json;

namespace eBookStore.Client.Services;
public class AuthService : IAuthService
{
	private readonly IBaseService _baseService;
	public AuthService(IBaseService baseService)
	{
		_baseService = baseService;
	}
	public async Task<LoginResponseModel?> LoginAsync(LoginModel model)
	{
		var result = await _baseService.SendAsync(new RequestModel
		{
			APIType = StaticDetails.APIType.POST,
			Data = model,
			URL = $"{StaticDetails.SERVICE_BASE_URL}/auth"
		});
		return string.IsNullOrEmpty(result) ? null :
			JsonConvert.DeserializeObject<LoginResponseModel>(result);


	}

}