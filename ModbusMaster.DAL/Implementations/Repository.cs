using ModbusMaster.DAL.Interfaces;
using ModbusMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ModbusMaster.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected ModbusDumpContext _context;

        public Repository(ModbusDumpContext context)
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
