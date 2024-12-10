using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.Dtos;
using ToDoList.Data.Entities;

namespace ToDoList.Infra.Queries
{
    public class GetTasksHandler
    {
        private readonly IUnitofWork _unitOfWork;

        public GetTasksHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskItem>> Handle(GetTasksQuery query)
        {
            var tasks = await _unitOfWork.Tasks.GetTasksByUserAsync(query.UserId);

            if (!string.IsNullOrEmpty(query.Status))
                tasks = tasks.Where(task => task.Status == query.Status);

            return tasks;
        }
    }
}
