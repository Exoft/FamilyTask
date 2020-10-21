using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Abstractions.Repositories;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class TaskRepository : BaseRepository<Guid, Task, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context) { }

        public new ITaskRepository Reset()
        {
            return base.Reset();
        }

        public async System.Threading.Tasks.Task<List<Task>> ByMemberIdAsync(Guid memberId,
            CancellationToken cancellationToken = default)
        {
            return await Query.Where(i => i.AssignedToId == memberId).ToListAsync(cancellationToken);
        }

        public override async System.Threading.Tasks.Task<IEnumerable<Task>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await
                Query.Include(i => i.AssignedTo).ToListAsync(cancellationToken);
        }

        public new ITaskRepository NoTrack()
        {
            return base.NoTrack();
        }
    }
}