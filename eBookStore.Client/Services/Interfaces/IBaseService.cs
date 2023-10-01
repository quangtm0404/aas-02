using eBookStore.Client.Models;

namespace eBookStore.Client.Services.Interfaces;
public interface IBaseService
{
    Task<object?> SendAsync(RequestModel request, bool withToken = false);
}