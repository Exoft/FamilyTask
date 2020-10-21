using System;

namespace Domain.Commands
{
    public class CompleteTaskForMemberCommand
    {
        public Guid TaskId { get; set; }

        public Guid MemberId { get; set; }
    }
}