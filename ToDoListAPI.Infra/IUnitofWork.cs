﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAPI.Infra
{
    public interface IUnitofWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        Task<int> CommitAsync();
    }
}
