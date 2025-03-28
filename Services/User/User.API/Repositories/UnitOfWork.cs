using Microsoft.EntityFrameworkCore;
using Ordering.API.Data;

namespace Ordering.API.Repositories
{
	internal class UnitOfWork : IUnitOfWork
	{
		private readonly OrderDbContext _dbContext;

		public UnitOfWork(OrderDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			FillCreatedEntities();
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		private void FillCreatedEntities()
		{
			var entries = _dbContext.ChangeTracker.Entries()
				.Where(e => e.Entity is BaseEntity && e.State == EntityState.Added);

			foreach (var entry in entries)
			{
				var entity = (BaseEntity)entry.Entity;
				entity.CreatedDate = DateTime.Now;
			}
		}
	}
}
