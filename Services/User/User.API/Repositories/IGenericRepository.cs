using System.Linq.Expressions;

namespace Ordering.API.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
		Task<TEntity?> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
		Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
		Task<List<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
		Task AddAsync(TEntity entity, CancellationToken cancellationToken);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
	}
}
