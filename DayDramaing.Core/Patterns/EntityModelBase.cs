using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Innovations.Core.Extensions;

namespace Innovations.Core.Patterns
{
    public abstract class EntityModelBase<T>
        where T : class
    {
        private List<MappedProperty<T>> mapper;

        public EntityModelBase() : this(null) { }

        public T Entity { get; set; }

        protected bool updating = false;

        public EntityModelBase(T entity)
        {
            mapper = new List<MappedProperty<T>>();
            Entity = entity;
        }

        protected abstract void SetupMapping();

        public void Map<TModel>(Expression<Func<TModel, object>> thisProperty, Expression<Func<T, object>> entityProperty, object entityToSet = null)
            where TModel : EntityModelBase<T>
        {

            entityToSet = entityToSet ?? Entity;

            var thisPropertyInfo = thisProperty.GetPropertyInfo();
            var entityInfo = entityProperty.GetPropertyInfo();

            //set mapped value
            if (!updating)
            {
                var value = entityProperty.Compile()(Entity);
                thisPropertyInfo.SetValue(this, value, null);
            }

            var reval = new Func<EntityModelBase<T>, object>(
                x =>
                {
                    return thisProperty.Compile()((TModel)this);
                });

            mapper.Add(new MappedProperty<T>()
            {
                EntityObject = entityToSet,
                EntityProperty = entityInfo,
                ModelFunc = reval
            });
        }

        public virtual void Update(T entity)
        {
            Entity = entity;
            updating = true;
            SetupMapping();
            UpdateEntityMapper();
            updating = false;
        }

        private void UpdateEntityMapper()
        {
            foreach (var m in mapper)
            {
                var value = m.ModelFunc(this);
                m.EntityProperty.SetValue(m.EntityObject, value, null);
            }
        }

        private void UpdateEntityReflection()
        {
            var modelProperties = this.GetType().GetProperties();
            var entityProperties = this.GetType().GetProperties();

            var join = from m in modelProperties
                       join e in entityProperties on new { m.Name, m.PropertyType } equals new { e.Name, e.PropertyType }
                       select new { Model = m, Entity = e };

            foreach (var item in join)
            {
                var value = item.Model.GetValue(this, null);
                item.Entity.SetValue(Entity, value, null);
            }
        }

        public event EventHandler<DeleteEventArgs> EntityDeleted;
        protected void OnDelete(object entity)
        {
            var handler = EntityDeleted;
            if (handler != null)
            {
                handler(this, new DeleteEventArgs(entity));
            }
        }
    }

    public class DeleteEventArgs : EventArgs
    {
        public object Entity { get; private set; }
        public DeleteEventArgs(object entity)
        {
            Entity = entity;
        }
    }

    public class MappedProperty<T>
        where T : class
    {
        public Func<EntityModelBase<T>, object> ModelFunc { get; set; }
        public PropertyInfo EntityProperty { get; set; }
        public object EntityObject { get; set; }
    }
}
