
using System.Net.Mime;

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
        } catch(Exception ex) 
        {
            context.Response.StatusCode = (int) StatusCodes.Status400BadRequest;
            context.Response.ContentType = "text";
            _logger.LogError($"--> Error: {ex.Message}");
            await context.Response.WriteAsync($"--> Error: {ex.Message}");
            

        }
    }
}