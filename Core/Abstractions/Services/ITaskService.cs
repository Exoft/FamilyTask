using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<AssignTaskToMemberCommandResult> AssignTaskToMemberCommandHandler(AssignTaskToMemberCommand command);

        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);

        Task<CompleteTaskCommandResult> CompleteTaskCommandHandler(CompleteTaskCommand command);

        Task<CompleteTaskForMemberCommandResult> CompleteTaskForMemberCommandHandler(
            CompleteTaskForMemberCommand command);

        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();

        Task<GetAllTasksByMemberQueryResult> GetAllTasksByMemberQueryHandler(GetAllTasksByMemberQuery query);
    }
}