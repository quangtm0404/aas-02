using System;
using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.AuthorViewModels;
using Newtonsoft.Json;

namespace eBookStore.Client.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IBaseService _baseService;
        public AuthorService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<AuthorViewModel?> CreateAsync(AuthorCreateModel model)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.POST,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/authors",
                Data = model
            });
            if (string.IsNullOrEmpty(result)) return null;
            else return JsonConvert.DeserializeObject<AuthorViewModel>(result);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.DELETE,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/authors/{id}"
            });
            if (result == "NoContent")
                return true;
            else return false;
        }

        public async Task<IEnumerable<AuthorViewModel>?> GetAllAsync(string search = "")
        {
            string? result = "";
            if (string.IsNullOrEmpty(search))
            {
                result = await _baseService.SendAsync(new Models.RequestModel
                {
                    APIType = StaticDetails.APIType.GET,
                    URL = $"{StaticDetails.SERVICE_BASE_URL}/authors"
                });
            } else 
            {
                result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.GET,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/authors?$filter=contains(firstName, '{search}') or contains(lastName, '{search}')"
            });
            }

            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<IEnumerable<AuthorViewModel>>(result);
            }
            else return null;
        }

        public async Task<AuthorViewModel?> GetByIdAsync(Guid id)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.GET,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/authors/{id}"

            });
            if (!string.IsNullOrEmpty(result))
                return JsonConvert.DeserializeObject<AuthorViewModel>(result);
            else return null;
        }

        public async Task<bool> UpdateAsync(AuthorUpdateModel model)
        {
            var result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.PUT,
                Data = model,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/authors"
            });
            if (result == "NoContent") return true;
            return false;
        }
    }
}
