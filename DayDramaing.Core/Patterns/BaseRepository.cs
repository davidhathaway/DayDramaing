using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Innovations.Core.Extensions;
namespace Innovations.Core.Patterns
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        protected PropertyInfo primaryKey;

        public IPrincipal CurrentUser { get; set; }

        public int CurrentUserId { get; set; }

        public BaseRepository()
        {

        }

        public BaseRepository(IPrincipal user)
        {
            CurrentUser = user;

        }

        protected virtual void Setup()
        {
            if (CurrentUser != null)
            {
                CurrentUserId = this.GetUserId(CurrentUser.GetUsername());
            }
            FindKey();
        }
        public abstract int GetUserId(string username);
        public abstract T Create(T entity);
        public void Create(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                Create(entity);
            }
        }

        public abstract T Update(T entity);
        public void Update(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }
        public abstract void Delete(T entity);
        public void Delete(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entites = FindAll(predicate);
            foreach (var entity in entites)
            {
                Delete(entity);
            }
        }

        public abstract void Save();

        public T FindById(int id, bool throwIfNull = false)
        {
            var predicate = WhereId(id);
            var entity = FindFirst(predicate);


            if (entity == null && throwIfNull)
            {
                throw new EntityIdNotFoundException(id, typeof(T));
            }
            return entity;
        }
        public abstract T FindSingle(Expression<Func<T, bool>> predicate);
        public abstract T FindFirst(Expression<Func<T, bool>> predicate);
        public abstract IQueryable<T> FindAll();
        public IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return FindAll().Where(predicate);
        }

        private void FindKey()
        {
            var key = OnFindKey();
            if (key == null)
            {
                throw new EntityKeyNotFoundException(typeof(T));
            }
            primaryKey = key;
        }
        protected virtual PropertyInfo OnFindKey()
        {
            var type = typeof(T);
            var name = type.Name;
            var idName = name + "Id";
            return type.GetProperty(idName);
        }

        #region Idisposable Members

        protected bool disposed;
        protected virtual void OnDispose()
        {
            disposed = true;
        }
        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        protected Expression<Func<T, bool>> WhereId(int id)
        {
            var type = typeof(T);
            var propInfo = primaryKey;
            ParameterExpression p1 = Expression.Parameter(type, "x"); //x=>
            var left = Expression.Property(p1, propInfo); //x.Key
            var right = Expression.Constant(id);
            var body = Expression.Equal(left, right); //x.Key == value

            var lambda = Expression.Lambda<Func<T, bool>>(body, p1); //x=>x.Key == value

            return lambda;
        }
    }

}
