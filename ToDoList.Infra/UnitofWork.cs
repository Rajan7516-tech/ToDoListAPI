using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;

namespace ToDoList.Infra
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }
        public ITaskRepository Tasks { get; }

        public UnitofWork(AppDbContext context, IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _context = context;
            Users = userRepository;
            Tasks = taskRepository;
        }

        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
