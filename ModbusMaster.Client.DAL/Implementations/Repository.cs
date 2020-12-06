using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.Client.DAL;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public Task<T> Get(int id)
        {
            return _context.Set<T>().SingleAsync(s => s.Id == id);
        }

        public Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public Task<List<T>> GetAll()
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToListAsync();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
