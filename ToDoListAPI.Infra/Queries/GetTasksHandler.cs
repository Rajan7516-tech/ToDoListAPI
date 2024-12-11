using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Data.Dtos;
using ToDoListAPI.Data.Entities;

namespace ToDoListAPI.Infra.Queries
{
    public class GetTasksHandler
    {
        private readonly IUnitofWork _unitOfWork;

        public GetTasksHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserTaskDto>> Handle(GetTasksQuery query)
        {
            if (query.UserId > 0)
            {
                var tasks = await _unitOfWork.Tasks.GetTasksByUserAsync(query.UserId);
                var users = await _unitOfWork.Users.GetAllAsync();
                if (!string.IsNullOrEmpty(query.Status))
                    tasks = tasks.Where(task => task.Status == query.Status);
                var result = (from task in tasks
                              join user in users on task.UserId equals user.Id
                              orderby task.Id
                              select new UserTaskDto
                              {
                                  Id = task.Id,
                                  UserID = task.UserId,
                                  Name = user.Username,
                                  TaskName = task.Title,
                                  TaskDescription = task.Description,
                                  TaskStatus = task.Status

                              }).ToList();

                return result;
            }
            else
            {
                var tasks = await _unitOfWork.Tasks.GetAllAsync();
                if (!string.IsNullOrEmpty(query.Status))
                    tasks = tasks.Where(task => task.Status == query.Status);
                var users = await _unitOfWork.Users.GetAllAsync();
                var result = (from task in tasks
                              join user in users on task.UserId equals user.Id
                              orderby task.Id
                              select new UserTaskDto
                              {
                                  Id = task.Id,
                                  UserID = task.UserId,
                                  Name = user.Username,
                                  TaskName = task.Title,
                                  TaskDescription = task.Description,
                                  TaskStatus = task.Status

                              }).ToList();
                
                return result;
            }
                
        }
    }
}
