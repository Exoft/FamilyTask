using System.Collections.Generic;
using Domain.ViewModel;

namespace Domain.Queries
{
    public class GetAllTasksQueryResult
    {
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}