using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Innovations.Core.Patterns
{
    public abstract class CodeFirstRepository<T, TContext> : BaseRepository<T>
        where T : class
        where TContext : DbContext, new()
    {
        public TContext Context { get; set; }
        public DbSet<T> DbSet { get; set; }

        public CodeFirstRepository(IPrincipal user)
            : this(new TContext(), user)
        {

        }

        public CodeFirstRepository(TContext context, IPrincipal user): base(user)
        {
            Context = context;
            DbSet = Context.Set<T>();
            Setup();
        }
       
        public override T Create(T entity)
        {
            return DbSet.Add(entity);
        }
       
        public override T Update(T entity)
        {
            //entity could be offline.
            var entry = this.Context.Entry<T>(entity);
            if (entry.State == System.Data.EntityState.Detached)
            {
                entry.State = System.Data.EntityState.Modified;
            }
            return entity;
        }
   
        public override void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }
  
        public override void Save()
        {
            Context.SaveChanges();
        }
      
        public override T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }
      
        public override T FindFirst(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
     
        public override IQueryable<T> FindAll()
        {
            return DbSet.AsQueryable();
        }


        public override int GetUserId(string username) { return 0; }
        
    }
}
