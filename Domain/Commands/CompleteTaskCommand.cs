﻿using System;

namespace Domain.Commands
{
    public class CompleteTaskCommand
    {
        public Guid TaskId { get; set; }
    }
}