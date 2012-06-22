using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innovations.Core.Patterns
{
    public class SearchModel : PageModel
    {
        public string SearchValue { get; set; }

        //public List<SearchParamBase> Params { get; set; }
    }


    //public class SearchParam<TProperty, TEntity> : SearchParamBase
    //    where TEntity : class
    //{

    //    public virtual Func<SearchParam<TProperty, TEntity>, IQueryable<TEntity>, IQueryable<TEntity>> Query { get; set; }

    //    public virtual Func<TEntity, TProperty> EntityPropertySelector
    //    {
    //        get;
    //        set;
    //    }
    //    public virtual Func<object, object> PropertySelector
    //    {
    //        get
    //        {
    //            return new Func<object, object>(o =>
    //            {
    //                return GetFuncSelectorHelper(o);
    //            });
    //        }
    //    }
    //    public TProperty TypedValue { get; set; }
    //    public override Type Type
    //    {
    //        get
    //        {
    //            return typeof(TProperty);
    //        }
    //    }
    //    public override object Value
    //    {
    //        get
    //        {
    //            return TypedValue;
    //        }
    //        set
    //        {
    //            TypedValue = (TProperty)value;
    //        }
    //    }
    //    private object GetFuncSelectorHelper(object entity)
    //    {
    //        if (entity is TEntity)
    //        {
    //            return EntityPropertySelector.Invoke((TEntity)entity);
    //        }
    //        else
    //        { 
    //            throw new InvalidCastException("object is not a type of " + entity.GetType().Name)
    //        }
    //    }
    //}

    //public abstract class SearchParamBase
    //{
    //    public virtual Func<object, object> PropertySelector { get; }
    //    public virtual string Name { get; set; }
    //    public virtual object Value { get; set; }
    //    public virtual Type Type { get; }
    //}
}
