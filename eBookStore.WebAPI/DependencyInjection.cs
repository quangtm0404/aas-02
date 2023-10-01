using eBookStore.Services.Services;
using eBookStore.Services.Services.Interfaces;
using eBookStore.WebAPI.Middlewares;
using eBookStore.WebAPI.Services;
using Microsoft.AspNetCore.OData;

namespace eBookStore.WebAPI
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddControllers().AddOData(opt => 
            {
                opt.Filter().Select().OrderBy().SetMaxTop(100).Expand();
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            return services;
        }
    }
}