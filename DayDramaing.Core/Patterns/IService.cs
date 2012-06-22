using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public interface IService<T> : IDisposable
        where T : class
    {
        T FindById(int id);

        T FindSingle(Expression<Func<T, bool>> predicate);
        T FindFirst(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);

        EntityServiceResult<T> Create(EntityModel<T> model);
        EntityServiceResult<T> Create(T entity);
        EntityServiceResult<T> Update(T entity);
        EntityServiceResult<T> Update(EntityModel<T> model);
        //EntityServiceResult<T> Remove(EntityModel<T> model, int id);
        ServiceResult Delete(T entity);
        IModelState ModelState { get; set; }
    }


}
