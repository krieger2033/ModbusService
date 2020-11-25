using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.PollAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(int id);

        T GetSingle(Expression<Func<T, bool>> predicate);

        List<T> GetAll();

        List<T> Find(Expression<Func<T, bool>> predicate);

        void Insert(T entity);

        void Delete(T entity);
    }
}
