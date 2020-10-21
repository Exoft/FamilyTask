using System;

namespace Domain.Commands
{
    public class AssignTaskToMemberCommand
    {
        public Guid MemberId { get; set; }

        public Guid TaskId { get; set; }
    }
}