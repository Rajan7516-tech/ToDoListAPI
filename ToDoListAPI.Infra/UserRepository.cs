using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Data;
using ToDoListAPI.Data.Entities;

namespace ToDoListAPI.Infra
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
    }
}
