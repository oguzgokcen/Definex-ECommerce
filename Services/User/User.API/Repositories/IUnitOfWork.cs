﻿namespace Ordering.API.Repositories
{
	public interface IUnitOfWork
	{
		Task SaveChangesAsync(CancellationToken cancellationToken = default);

	}
}
