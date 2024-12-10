using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Data.Entities;

namespace ToDoListAPI.Infra.Commands
{
    public class CreateTaskHandler
    {
        private readonly IUnitofWork _unitOfWork;

        public CreateTaskHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItem> Handle(CreateTaskCommand command)
        {
            var task = new TaskItem
            {
                Title = command.Title,
                Description = command.Description,
                UserId = command.UserId
            };

            _unitOfWork.Tasks.Add(task);
            await _unitOfWork.CommitAsync();

            return task;
        }
    }
}
