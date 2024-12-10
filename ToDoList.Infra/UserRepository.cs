using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.Entities;
using ToDoList.Data;

namespace ToDoList.Infra
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
