using System;

namespace Domain.DataModels
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public bool IsComplete { get; set; }

        public Guid? AssignedToId { get; set; }

        public Member AssignedTo { get; set; }
    }
}