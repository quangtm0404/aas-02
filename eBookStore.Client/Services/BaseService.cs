using eBookStore.Client.Models;
using eBookStore.Client.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static eBookStore.Client.StaticDetails;

namespace eBookStore.Client.Services;
public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;
    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }
    public async Task<string?> SendAsync(RequestModel request, bool withToken = true)
    {
        HttpClient client = _httpClientFactory.CreateClient("eBookStore");
        HttpRequestMessage message = new();
        message.Headers.Add("Accept", "application/json");

        // Token
        if (withToken)
        {
            var accessToken = _tokenProvider.GetToken();
            message.Headers.Add("Authorization", $"Bearer {accessToken}");
        }
        message.RequestUri = new Uri(request.URL);
        if (request.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), encoding: Encoding.UTF8, "application/json");
        }

        HttpResponseMessage? apiResponse = null;
        switch (request.APIType)
        {
            case APIType.GET:
                message.Method = HttpMethod.Get;
                break;
            case APIType.POST:
                message.Method = HttpMethod.Post;
                break;
            case APIType.PUT:
                message.Method = HttpMethod.Put;
                break;
            case APIType.DELETE:
                message.Method = HttpMethod.Delete;
                break;
            default:
                message.Method = HttpMethod.Get;
                break;
        }

        apiResponse = await client.SendAsync(message);

        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return string.Empty;
            case HttpStatusCode.Forbidden:
                return string.Empty;
            case HttpStatusCode.Unauthorized:
                return string.Empty;
            case HttpStatusCode.InternalServerError:
                return string.Empty;
            case HttpStatusCode.BadRequest:
                return string.Empty;
            case HttpStatusCode.UnsupportedMediaType:
                return string.Empty;
            case HttpStatusCode.NoContent:
                return "NoContent";
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                return apiContent;
        }
    }

}

