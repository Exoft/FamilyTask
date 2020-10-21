using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;

namespace WebApi.AutoMapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Task>();
            CreateMap<Task, TaskVm>().ConvertUsing<TaskToTaskVmConverter>();
        }
        
        public class TaskToTaskVmConverter : ITypeConverter<Task, TaskVm>
        {
            public TaskVm Convert(Task source, TaskVm destination, ResolutionContext context)
            {
                return new TaskVm
                {
                    Id = source.Id,
                    Subject = source.Subject,
                    AssignedTo = context.Mapper.Map<MemberVm>(source.AssignedTo),
                    IsComplete = source.IsComplete
                };
            }
        }
    }
}