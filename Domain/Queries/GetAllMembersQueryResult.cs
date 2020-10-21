using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetAllMembersQueryResult
    {
        public IEnumerable<MemberVm> Payload { get; set; }        
    }
}
