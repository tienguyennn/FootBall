using Microsoft.EntityFrameworkCore;
using N.Model.Entities;

namespace N.Service.Common.Service
{
    public interface IService<T> where T : Entity
    {
        T? GetById(Guid? id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetQueryable();
    }
}
