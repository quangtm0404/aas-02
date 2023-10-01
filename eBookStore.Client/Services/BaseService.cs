using System.Net;
using System.Text.Json;
using eBookStore.Client.Models;
using eBookStore.Client.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
    public async Task<object?> SendAsync(RequestModel request, bool withToken = false)
    {
        HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
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
            message.Content = new StringContent(JsonSerializer.Serialize(request.Data));
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
                return null;
            case HttpStatusCode.Forbidden:
                return null;
            case HttpStatusCode.Unauthorized:
                return null;
            case HttpStatusCode.InternalServerError:
                return null;
            case HttpStatusCode.BadRequest:
                return null;
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseModel = JsonSerializer.Deserialize<object?>(apiContent);
                return apiResponseModel;
        }
    } 

}

