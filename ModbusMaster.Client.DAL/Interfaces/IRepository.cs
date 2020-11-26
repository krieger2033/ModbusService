using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ModbusMaster.Domain.Entities;

namespace ModbusMaster.Client.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Get(int id);

        Task<T> GetSingle(Expression<Func<T, bool>> predicate);

        Task <List<T>> GetAll();

        Task <List<T>> Find(Expression<Func<T, bool>> predicate);

        void Insert(T entity);

        void Delete(T entity);
    }
}
