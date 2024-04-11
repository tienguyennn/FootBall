using Microsoft.EntityFrameworkCore;
using N.Model.Entities;

namespace N.Service.Common.Service
{
    public interface IService<T> where T : class
    {
        T? GetById(Guid? id);
        Task Create(T entity);
        Task Create(IEnumerable<T> entities);
        Task Update(T entity);
        Task Update(IEnumerable<T> entities);
        Task Delete(T entity);
        Task Delete(IEnumerable<T> entities);
        IQueryable<T> GetQueryable();
    }
}
