using eBookStore.Repositories.Data;
using eBookStore.Repositories.Repositories;
using eBookStore.Services;
using eBookStore.Services.Profiles;
using eBookStore.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eBookStore.Repositories
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, string dbConnection) 
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection));
            services.AddAutoMapper(typeof(Profiles).Assembly);

            #region  DI Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>()
                    .AddScoped<IBookRepository, BookRepository>()
                    .AddScoped<IBookAuthorRepository, BookAuthorRepository>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IRoleRepository, RoleRepository>()
                    .AddScoped<IPublisherRepository, PublisherRepository>()
                    .AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            return services;
        }
    }
}