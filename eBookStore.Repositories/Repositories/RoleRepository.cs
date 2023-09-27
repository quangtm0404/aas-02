using eBookStore.Domains.Entities;
using eBookStore.Repositories.Data;
using eBookStore.Services.Repositories;
using eBookStore.Services.Services.Interfaces;

namespace eBookStore.Repositories.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext, IClaimsService claimsService, ICurrentTime currentTime) : base(dbContext, currentTime, claimsService)
        {
            
        }
    }
}