using eBookStore.Services.Services;
using eBookStore.Services.Services.Interfaces;
using eBookStore.WebAPI.Services;

namespace eBookStore.WebAPI
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddWebAPIServices(this IServiceCollection services)
        {
            services.AddControllers()     ;
            services.AddHttpContextAccessor();
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            return services;
        }
    }
}