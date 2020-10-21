using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Shared.Models;

namespace WebClient.Abstractions
{
    public interface ITaskDataService
    {
        event EventHandler TasksUpdated;

        Task<List<TaskModel>> LoadData();

        Guid SelectedTask { get; set; }

        Task CreateTask(TaskModel model);
        Task AssignTaskToMember(Guid taskId, Guid memberId);
        Task CompleteTask(Guid taskId);
        Task CompleteForMemberTask(Guid taskId, Guid memberId);
    }
}