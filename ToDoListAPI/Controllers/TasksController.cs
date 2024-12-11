using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data.Entities;
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
        private readonly UpdateHandler _updateHandler;
        private readonly DeleteHandler _deleteHandler;
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

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            _updateHandler.Handle(id, updatedTask);
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            //if (task == null)
            //    return NotFound();

            //task.Title = updatedTask.Title;
            //task.Description = updatedTask.Description;
            //task.Status = updatedTask.Status;

            //_context.SaveChanges();

            return Ok("Task updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _deleteHandler.Handle(id);
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            //if (task == null)
            //    return NotFound();

            //_context.Tasks.Remove(task);
            //_context.SaveChanges();

            return Ok("Task deleted successfully.");
        }
    }
}
