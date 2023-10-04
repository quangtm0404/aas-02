using System.Text.Json;

namespace eBookStore.WebAPI.Middlewares;
public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;
            _logger.LogError(ex.Message);
            var result = JsonSerializer.Serialize(new ResponseModel
            {
                IsSuccess = false,
                Message = ex.Message
            });
            await context.Response.WriteAsync(result);


        }
    }
}