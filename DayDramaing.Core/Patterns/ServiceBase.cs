using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public abstract class ServiceBase<TEntity, TRepo> : IService<TEntity>
        where TEntity : class
        where TRepo : IRepository<TEntity>
    {
        public IModelState ModelState { get; set; }

        public int CurrentUserId { get; set; }

        protected TRepo Repository { get; set; }

        protected abstract TRepo CreateRepository();

        public ServiceBase()
        {
            Repository = CreateRepository();
        }

        public ServiceBase(IModelState modelState) 
        {
            ModelState = modelState;
            Repository = CreateRepository();
            if (ModelState != null)
            {
                CurrentUserId = Repository.GetUserId(ModelState.Username);
            }
        }


        public ServiceBase(TRepo repository)
            : this(repository, null)
        {

        }
        public ServiceBase(TRepo repository, IModelState modelState)
        {
            Repository = repository;
            ModelState = modelState;

            if (ModelState != null)
            {
                CurrentUserId = Repository.GetUserId(ModelState.Username);
            }
        }

        public TEntity FindById(int id)
        {
            return Repository.FindById(id);
        }

        public virtual EntityServiceResult<TEntity> Create(EntityModel<TEntity> model)
        {
            return RunWithValidation(model, null,
                (e) =>
                {
                    Repository.Create(e);
                    Repository.Save();
                    return e;
                });
        }
        public virtual EntityServiceResult<TEntity> Create(TEntity entity)
        {
            return RunWithValidation(entity,
                () =>
                {
                    Repository.Create(entity);
                    Repository.Save();
                    return entity;
                });
        }

        public abstract bool Validate(TEntity entity);

        public virtual EntityServiceResult<TEntity> Update(TEntity entity)
        {
            return RunWithValidation(entity,
                () =>
                {
                    Repository.Update(entity);
                    Repository.Save();
                    return entity;
                });
        }
        public virtual EntityServiceResult<TEntity> Update(EntityModel<TEntity> model)
        {
            var entity = FindById(model.Id);
            return RunWithValidation(model, entity,
               (e) =>
               {
                   Repository.Update(e);
                   Repository.Save();
                   return entity;
               });
        }

        public virtual ServiceResult Delete(TEntity entity)
        {
            var result = ServiceResult.Run(() =>
            {
                Repository.Delete(entity);
                Repository.Save();
            });

            if (result.Exception != null)
            {
                ModelState.AddError("Exception", result.Exception.ToString());
            }

            return result;
        }

        #region Disposable
        protected bool disposed;
        protected virtual void OnDispose()
        {
            if (Repository != null && !disposed)
            {
                Repository.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        public TEntity FindSingle(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindSingle(predicate);
        }
        public TEntity FindFirst(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindFirst(predicate);
        }
        public IQueryable<TEntity> FindAll()
        {
            return Repository.FindAll();
        }
        public IQueryable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.FindAll(predicate);
        }

        private EntityServiceResult<TEntity> RunWithValidation(TEntity entity, Func<TEntity> func)
        {
            var result = new EntityServiceResult<TEntity>();
            result.Success = false;
            if (Validate(entity))
            {
                result = EntityServiceResult<TEntity>.Run(func);

                if (result.Exception != null)
                {
                    ModelState.AddError("Exception", result.Exception.ToString());
                }
            }
            return result;
        }
        private EntityServiceResult<TEntity> RunWithValidation(EntityModel<TEntity> model, TEntity entity, Func<TEntity, TEntity> func)
        {
            var result = new EntityServiceResult<TEntity>();
            result.Success = false;
            try
            {
                if (entity == null)
                {
                    //model -> to entity
                    entity = model.ModelToEntity<TEntity>();
                }
                else
                {
                    //model -> to entity
                    entity = model.ModelToEntity<TEntity>(entity);
                }
      
                //validate
                if (Validate(entity))
                {
                    result = EntityServiceResult<TEntity>.Run(() => { return func(entity); });

                    if (result.Exception != null)
                    {
                        ModelState.AddError("Exception", result.Exception.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddError("Exception", ex.ToString());
                result.Exception = ex;
            }

            return result;
        }
    }
}
