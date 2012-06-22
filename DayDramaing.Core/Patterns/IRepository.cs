using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public interface IRepository<T> : IDisposable
        where T: class
    {
        //crud - unit of work
        T Create(T entity);
        void Create(IList<T> entities);

        T Update(T entity);
        void Update(IList<T> entities);

        void Delete(int id);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void Delete(IList<T> entities);

        void Save();

        //repository part
        T FindById(int id, bool throwIfNull = false);
        T FindSingle(Expression<Func<T, bool>> predicate);
        T FindFirst(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);

        //userid for user
        int GetUserId(string username);

        int CurrentUserId { get; set; }
    }
}
