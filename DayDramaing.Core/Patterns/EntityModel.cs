using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Innovations.Core.Extensions;

namespace Innovations.Core.Patterns
{
    public abstract class EntityModel<TEntity>
     where TEntity : class
    {
        public abstract int Id { get; }

        public virtual bool CanMapTo<TDest>()
        {
            var source = this.GetType();
            var dest = typeof(TDest);
            var map = Mapper.FindTypeMapFor(source, dest);
            return map != null;
        }
        public virtual bool CanMapFrom<TSource>()
        {
            var source = typeof(TSource);
            var dest = this.GetType();
            var map = Mapper.FindTypeMapFor(source, dest);
            return map != null;
        }

        public TDest ModelToEntity<TDest>()
              where TDest : class
        {
            if (CanMapTo<TDest>())
            {
                return this.Map<TDest>();
            }
            else
            {
                throw new ArgumentException("entity", "Entity Not mapped");
            }
        }
        public TDest ModelToEntity<TDest>(TDest entity)
            where TDest: class
        {
            if (CanMapTo<TDest>())
            {
                return this.Map<TDest>(entity);
            }
            else
            {
                throw new ArgumentException("entity", "Entity Not mapped");
            }
        }
    }

    //public abstract class CreateEntityModel<TEntity> : EntityModel<TEntity>
    //    where TEntity : class, new()
    //{
    //}

    //public abstract class EditEntityModel<TEntity> : EntityModel<TEntity>
    //    where TEntity : class, new()
    //{
      
        
    //}
}
