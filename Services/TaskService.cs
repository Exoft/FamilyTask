using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using Task = Domain.DataModels.Task;

namespace Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IMemberRepository _memberRepository;

        public TaskService(IMapper mapper, ITaskRepository taskRepository, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _memberRepository = memberRepository;
        }

        public async Task<AssignTaskToMemberCommandResult> AssignTaskToMemberCommandHandler(
            AssignTaskToMemberCommand command)
        {
            var task = await _taskRepository.ByIdAsync(command.TaskId);
            var member = await _memberRepository.ByIdAsync(command.MemberId);

            task.AssignedToId = command.MemberId;
            task.AssignedTo = member;

            var updatingResult = await _taskRepository.UpdateRecordAsync(task);

            return new AssignTaskToMemberCommandResult
            {
                IsSucceed = updatingResult != 0
            };
        }

        public async Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command)
        {
            var task = _mapper.Map<Task>(command);
            var persistedTask = await _taskRepository.CreateRecordAsync(task);

            var vm = _mapper.Map<TaskVm>(persistedTask);

            return new CreateTaskCommandResult
            {
                Payload = vm
            };
        }

        public async Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command)
        {
            var task = await _taskRepository.ByIdAsync(command.TaskId);

            task.IsComplete = !task.IsComplete;

            var updatingResult = await _taskRepository.UpdateRecordAsync(task);

            return new CompleteTaskCommandResult
            {
                IsSucceed = updatingResult != 0
            };
        }

        public async Task<CompleteTaskForMemberCommandResult> CompleteTaskForMemberCommandHandler(
            CompleteTaskForMemberCommand command)
        {
            var task = await _taskRepository.ByIdAsync(command.TaskId);

            task.IsComplete = !task.IsComplete;

            var updatingResult = await _taskRepository.UpdateRecordAsync(task);

            return new CompleteTaskForMemberCommandResult
            {
                IsSucceed = updatingResult != 0
            };
        }

        public async Task<GetAllTasksQueryResult> GetAllTasksQueryHandler()
        {
            var vm = new List<TaskVm>();

            var tasks = await _taskRepository.Reset().ToListAsync();

            if (tasks != null && tasks.Any())
            {
                vm = _mapper.Map<List<TaskVm>>(tasks);
            }

            return new GetAllTasksQueryResult
            {
                Payload = vm
            };
        }

        public async Task<GetAllTasksByMemberQueryResult> GetAllTasksByMemberQueryHandler(
            GetAllTasksByMemberQuery query)
        {
            var vm = new List<TaskVm>();

            var tasks = await _taskRepository.Reset().NoTrack().ByMemberIdAsync(query.MemberId);

            if (tasks != null && tasks.Any())
            {
                vm = _mapper.Map<List<TaskVm>>(tasks);
            }

            return new GetAllTasksByMemberQueryResult
            {
                Payload = vm
            };
        }
    }
}