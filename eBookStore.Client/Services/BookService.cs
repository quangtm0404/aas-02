using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.BookAuthorModels;
using eBookStore.Services.ViewModels.BookModels;
using Newtonsoft.Json;

namespace eBookStore.Client.Services;
public class BookService : IBookService
{
    private readonly IBaseService _baseService;
    public BookService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<bool> AddBookAuthor(BookAuthorCreateModel model)
    {

        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.POST,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/bookauthors",
            Data = model
        });
        if (!string.IsNullOrEmpty(result))
        {
            return true;
        }
        return false;
    }

    public async Task<BookViewModel?> CreateAsync(BookCreateModel model)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.POST,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/books",
            Data = model
        });
        if (string.IsNullOrEmpty(result)) return null;
        return JsonConvert.DeserializeObject<BookViewModel>(result);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.DELETE,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/books/{id}"
        });
        if (string.IsNullOrEmpty(result)) return false;
        return result == "NoContent";
    }

    public async Task<IEnumerable<BookViewModel>?> GetAllAsync(string query = "")
    {
        string? result = "";
        if (string.IsNullOrEmpty(query))
        {
            result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.GET,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/books"
            });
        } 
        else {
            result = await _baseService.SendAsync(new Models.RequestModel
            {
                APIType = StaticDetails.APIType.GET,
                URL = $"{StaticDetails.SERVICE_BASE_URL}/books?$filter=contains(title, '{query}')or contains(type, '{query}')"
            });
        }
    
        if (string.IsNullOrEmpty(result)) return null;
        else return JsonConvert.DeserializeObject<IEnumerable<BookViewModel>>(result);
    }

    public async Task<BookViewModel?> GetByIdAsync(Guid id)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.GET,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/books/{id}"
        });
        if (string.IsNullOrEmpty(result)) return null;
        else return JsonConvert.DeserializeObject<BookViewModel>(result);
    }

    public async Task<bool> UpdateAsync(BookUpdateModel model)
    {
        var result = await _baseService.SendAsync(new Models.RequestModel
        {
            APIType = StaticDetails.APIType.PUT,
            URL = $"{StaticDetails.SERVICE_BASE_URL}/books",
            Data = model
        });
        if (string.IsNullOrEmpty(result)) return false;
        return result == "NoContent";
    }
}