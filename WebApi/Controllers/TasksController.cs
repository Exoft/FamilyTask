using System;
using System.Threading.Tasks;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(GetAllTasksQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksQueryHandler();

            return Ok(result);
        }
        
        [HttpGet("{memberId}")]
        [ProducesResponseType(typeof(GetAllTasksQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByMember([FromRoute] Guid memberId)
        {
            var result = await _taskService.GetAllTasksByMemberQueryHandler(new GetAllTasksByMemberQuery
            {
                MemberId = memberId
            });

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);

            return Created($"/api/Tasks/{result.Payload.Id}", result);
        }

        [HttpPut("{taskId}/assign-to/{memberId}")]
        [ProducesResponseType(typeof(AssignTaskToMemberCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignTaskToMember([FromRoute] Guid taskId, [FromRoute] Guid memberId)
        {
            var result = await _taskService.AssignTaskToMemberCommandHandler(new AssignTaskToMemberCommand
            {
                MemberId = memberId,
                TaskId = taskId
            });

            if (!result.IsSucceed)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(CompleteTaskCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompleteTask([FromRoute] Guid taskId)
        {
            var result = await _taskService.CompleteTaskCommandHandler(new CompleteTaskCommand
            {
                TaskId = taskId,
            });

            if (!result.IsSucceed)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{taskId}/complete-for/{memberId}")]
        [ProducesResponseType(typeof(CompleteTaskForMemberCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompleteForMemberTask([FromRoute] Guid taskId, [FromRoute] Guid memberId)
        {
            var result = await _taskService.CompleteTaskForMemberCommandHandler(new CompleteTaskForMemberCommand
            {
                TaskId = taskId,
                MemberId = memberId
            });

            if (!result.IsSucceed)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}