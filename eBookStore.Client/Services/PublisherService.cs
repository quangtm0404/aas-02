using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.PublisherModels;
using Newtonsoft.Json;

namespace eBookStore.Client.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IBaseService _baseService;
        public PublisherService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<PublisherViewModel?> CreateAsync(PublisherCreateModel model)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.POST,
                Data = model,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers"
            });
            return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<PublisherViewModel>(result) : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.DELETE,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers/{id}"
            });
            if (!string.IsNullOrEmpty(result))
                return result == "NoContent" ? true : false;
            return false;
        }

        public async Task<IEnumerable<PublisherViewModel>?> GetAllAsync(string search = "")
        {
            string? stringResult = "";
            if (string.IsNullOrEmpty(search))
            {
                stringResult = await _baseService.SendAsync(new Models.RequestModel
                {
                    APIType = StaticDetails.APIType.GET,
                    URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers"
                });
            } else 
            {
                stringResult = await _baseService.SendAsync(new Models.RequestModel
                {
                    APIType = StaticDetails.APIType.GET,
                    URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers?$filter=contains(name, '{search}')"
                });
            }

            return JsonConvert.DeserializeObject<IEnumerable<PublisherViewModel>>(stringResult!);
        }

        public async Task<PublisherViewModel?> GetByIdAsync(Guid id)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            { APIType = StaticDetails.APIType.GET, URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers/{id}" });
            if (!string.IsNullOrEmpty(result))
                return JsonConvert.DeserializeObject<PublisherViewModel>(result);
            return null;
        }

        public async Task<bool> UpdateAsync(PublisherUpdateModel model)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.PUT,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/publishers",
                Data = model
            });
            return string.IsNullOrEmpty(result) ? false : (result == "NoContent" ? true : false);
        }
    }
}
