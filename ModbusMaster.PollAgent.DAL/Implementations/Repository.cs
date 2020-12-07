using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ModbusMaster.Domain.Entities;
using ModbusMaster.PollAgent.DAL.Interfaces;

namespace ModbusMaster.PollAgent.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected ModbusPollContext _context;

        public Repository(ModbusPollContext context)
        {
            _context = context;
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Single(s => s.Id == id);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public virtual void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
