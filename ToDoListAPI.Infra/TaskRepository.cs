using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;
using ToDoListAPI.Data.Entities;

namespace ToDoListAPI.Infra
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByUserAsync(int userId);
    }
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserAsync(int userId)
        {
            return await _dbSet.Where(task => task.UserId == userId).ToListAsync();
        }
    }
}
