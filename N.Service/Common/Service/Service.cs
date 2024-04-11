using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using N.Model;
using N.Repository;
using System.Linq.Expressions;
using System;

namespace N.Service.Common.Service
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T? GetById(Guid? guid)
        {
            if (guid == null)
            {
            }

            return _repository.GetById(guid.Value);
        }
        public virtual async Task Create(T entity)
        {
            var iEntity = entity as Entity;
            if (iEntity != null)
            {
                iEntity.CreatedDate = DateTime.Now;
            }
            _repository.Add(entity);
            await _repository.Save();
        }

        public virtual async Task Create(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Add(entity);
            }
            await _repository.Save();
        }

        public virtual async Task Update(T entity)
        {
            _repository.Edit(entity);
            await _repository.Save();
        }

        public virtual async Task Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Edit(entity);
            }
            await _repository.Save();
        }
        public async Task Delete(T entity)
        {
            _repository.Delete(entity);
            await _repository.Save();
        }

        public async Task Delete(IEnumerable<T> entities)
        {
            _repository.DeleteRange(entities);
            await _repository.Save();
        }
        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetQueryable().Where(predicate);
        }
        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetQueryable().FirstOrDefault(predicate);
        }
        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetQueryable().Count(predicate);
        }


    }
}
