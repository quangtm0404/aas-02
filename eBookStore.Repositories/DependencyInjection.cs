using eBookStore.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eBookStore.Repositories
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, string dbConnection) 
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            return services;
        }
    }
}