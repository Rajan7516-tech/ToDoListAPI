using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;

namespace ToDoListAPI.Infra
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}
