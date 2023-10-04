using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.RoleViewModels;
using Newtonsoft.Json;

namespace eBookStore.Client.Services
{
    public class RoleService : IRoleService
    {
        private readonly IBaseService _baseService;

        public RoleService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<IEnumerable<RoleViewModel>?> GetAllRoleAsync()
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.GET,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/roles"
            });
            return JsonConvert.DeserializeObject<IEnumerable<RoleViewModel>>(result!);
        }
    }
}
