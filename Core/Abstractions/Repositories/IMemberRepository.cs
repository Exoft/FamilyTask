using Domain.DataModels;
using System;

namespace Core.Abstractions.Repositories
{
    public interface IMemberRepository : IBaseRepository<Guid, Member, IMemberRepository>
    {
    }
}
