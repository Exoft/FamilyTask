using System.Collections.Generic;
using Domain.ViewModel;

namespace Domain.Queries
{
    public class GetAllTasksByMemberQueryResult
    {
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}