using eBookStore.Domains.Entities;
using eBookStore.Repositories.Data;
using eBookStore.Repositories.Repositories.Interfaces;
using eBookStore.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eBookStore.Repositories.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public GenericRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _dbSet = context.Set<TEntity>();
            _timeService = timeService;
            _claimsService = claimsService;
        }
        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes) =>
            await includes
           .Aggregate(_dbSet.AsQueryable(),
               (entity, property) => entity.Include(property).IgnoreAutoIncludes())
           .Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreationDate)
           .ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
               .Aggregate(_dbSet.AsQueryable(),
                   (entity, property) => entity.Include(property))
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreationDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.GetCurrentUser;
            var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }

        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteBy = _claimsService.GetCurrentUser;
            entity.DeletionDate = _timeService.GetCurrentTime();
            _dbSet.Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.ModificationDate = _timeService.GetCurrentTime();
            entity.ModificationBy = _claimsService.GetCurrentUser;
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUser;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimsService.GetCurrentUser;
            }
            _dbSet.UpdateRange(entities);
        }

      

        public void UpdateRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.GetCurrentUser;
            }
            _dbSet.UpdateRange(entities);
        }

        public async Task<TEntity> FindByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        => await includes
           .Aggregate(_dbSet!.AsQueryable(),
               (entity, property) => entity!.Include(property)).AsNoTracking()
           .Where(expression!)
            .FirstAsync(x => x.IsDeleted == false);

        public async Task<List<TEntity>> FindListByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        => await includes
           .Aggregate(_dbSet!.AsQueryable(),
               (entity, property) => entity.Include(property)).AsNoTracking()
           .Where(expression!)
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync();
	}
}