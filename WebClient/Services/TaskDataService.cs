using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Components;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient _httpClient;

        public Guid SelectedTask { get; set; }

        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
        }

        public event EventHandler TasksUpdated;

        public async Task<List<TaskModel>> LoadData()
        {
            var data = await GetAllTasks();
            return data?.Payload?.Select(i => new TaskModel
            {
                Id = i.Id,
                Member = i.AssignedTo?.Id,
                Text = i.Subject,
                IsDone = i.IsComplete,
                Avatar = i.AssignedTo?.Avatar
            }).ToList();
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("Tasks");
        }

        public async Task CreateTask(TaskModel model)
        {
            await _httpClient.PostJsonAsync("tasks",
                new CreateTaskCommand
                {
                    Subject = model.Text
                });

            await LoadData();
            
            TasksUpdated?.Invoke(this, EventArgs.Empty);
        }

        public async Task AssignTaskToMember(Guid taskId, Guid memberId)
        {
            await _httpClient.PutJsonAsync($"tasks/{taskId}/assign-to/{memberId}", null);

            await LoadData();

            TasksUpdated?.Invoke(this, EventArgs.Empty);
        }

        public async Task CompleteTask(Guid taskId)
        {
            await _httpClient.PutJsonAsync($"tasks/{taskId}", null);
        }

        public async Task CompleteForMemberTask(Guid taskId, Guid memberId)
        {
            await _httpClient.PutJsonAsync($"tasks/{taskId}/complete-for/{memberId}", null);
        }
    }
}