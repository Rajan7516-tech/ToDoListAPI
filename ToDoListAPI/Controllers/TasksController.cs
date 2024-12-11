using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Infra.Commands;
using ToDoListAPI.Infra.Queries;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly CreateTaskHandler _createHandler;
        private readonly GetTasksHandler _queryHandler;

        public TasksController(CreateTaskHandler createHandler, GetTasksHandler queryHandler)
        {
            _createHandler = createHandler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            var taskId = await _createHandler.Handle(command);
            return Ok(new { TaskId = taskId });
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery][Optional] int userId, [FromQuery] string status)
        {
            var query = new GetTasksQuery { UserId = userId, Status = status };
            var tasks = await _queryHandler.Handle(query);
            return Ok(tasks);
        }
        //public async Task<IActionResult> DeleteTask(DeleteTaskCommand command)
        //{
        //    var taskId = await _deleteHandler.Handle(command);

        //    return Ok(GetTasks);
        //}
    }
}
