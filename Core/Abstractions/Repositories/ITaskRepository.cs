using System;
using System.Collections.Generic;
using System.Threading;
using Domain.DataModels;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Task, ITaskRepository>
    {
        /// <summary>
        /// Get the record that has a matching Key.
        /// </summary>
        /// <param name="memberId">The key to search for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Guid"/></returns>
        System.Threading.Tasks.Task<List<Task>> ByMemberIdAsync(Guid memberId,
            CancellationToken cancellationToken = default);
    }
}