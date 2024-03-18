using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using System.Linq.Expressions;

namespace N.Repository
{
    public class Repository<T> : IRepository<T>
       where T : class
    {
        protected DbContext _entities;
        protected readonly DbSet<T> _dbset;

        public Repository(DbContext context)
        {

            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual DbSet<T> DBSet()
        {
            return _dbset;
        }
        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.AsEnumerable<T>();
        }
        public virtual IQueryable<T> GetQueryable()
        {
            return _dbset.AsQueryable<T>();
        }
        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {

            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity).Entity;
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Save()
        {
            await _entities.SaveChangesAsync();
        }

        public T? GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                if (_entities.Entry(item).State == EntityState.Detached)
                {
                    _dbset.Attach(item);
                }
                _dbset.Remove(item);
            }
        }
    }
}
