
using System.Linq.Expressions;
using eBookStore.Domains.Entities;

namespace eBookStore.Services.Repositories;

	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
		Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
		Task<TEntity> AddAsync(TEntity entity);
		void Update(TEntity entity);
		void UpdateRange(List<TEntity> entities);
		void SoftRemove(TEntity entity);
		Task AddRangeAsync(List<TEntity> entities);
		void SoftRemoveRange(List<TEntity> entities);
		Task<TEntity> FindByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

		Task<List<TEntity>> FindListByField(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
	}
