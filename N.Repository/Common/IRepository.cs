using N.Model.Entities;
using System.Linq.Expressions;

namespace N.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetQueryable();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        Task Save();
        T? GetById(Guid id);
        void DeleteRange(IEnumerable<T> entities);
    }
}
