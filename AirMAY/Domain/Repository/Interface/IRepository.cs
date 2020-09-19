using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirMAY.Domain.Repository.Interface
{
    interface IRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> FindByConditionAsync(Expression<Func<T, bool>> predicat);
        Task Add(T obj);
        Task Change(T obj);
        Task Remove(T obj);
    }
}