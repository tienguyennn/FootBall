using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using N.Model;
using N.Repository;
using System.Linq.Expressions;
using System;

namespace N.Service.Common.Service
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T? GetById(Guid? guid)
        {
            if(guid == null)
            {
                return null;
            }

            return _repository.GetById(guid.Value);
        }
        public virtual void Create(T entity)
        {
            _repository.Add(entity);
            _repository.Save();
        }

        public virtual void Create(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Add(entity);
            }
            _repository.Save();
        }

        public virtual void Update(T entity)
        {
            _repository.Edit(entity);
            _repository.Save();
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Edit(entity);
            }
            _repository.Save();
        }
        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _repository.Save();
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
