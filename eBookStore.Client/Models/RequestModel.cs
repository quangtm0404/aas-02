using static eBookStore.Client.StaticDetails;

namespace eBookStore.Client.Models;
public class RequestModel
{
    public APIType APIType { get; set; } = APIType.GET;
    public string URL { get; set; } = default!;
    public object Data { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
}