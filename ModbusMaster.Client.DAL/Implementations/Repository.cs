using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ModbusMaster.Client.DAL.Interfaces;
using ModbusMaster.DAL;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApiDataContext _context;

        public Repository(ApiDataContext context)
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
