using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public class EntityServiceResult<T> : ServiceResult
    {
        public T Entity { get; set; }
        public static EntityServiceResult<TEntity> Run<TEntity>(Func<TEntity> func)
            where TEntity : class
        {
            var result = new EntityServiceResult<TEntity>();
            result.Success = false;
            try
            {
                result.Entity = func();
                result.Success = true;
            }
            catch (Exception ex)
            {
                //if (HttpContext.Current != null)
                //{
                //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                //}
                result.Exception = ex;
            }
            return result;
        }
    }
}
