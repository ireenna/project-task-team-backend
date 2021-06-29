using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL
{
    public class Repository<TEntity>:IRepository<TEntity>
    {
        protected readonly ThreadContext context;
        public Repository()
        {
            context = ThreadContext.getInstance();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }
        void Create(TEntity entity, string createdBy = null)
        {
            context.Set<TEntity>().Add(entity);
        }
        void Update(TEntity entity, string updatedBy = null)
        {
            context.Set<TEntity>().Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
        }
        void Delete(object id)
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }
        void Delete(TEntity entity)
        {
            var dbSet = context.Set<TEntity>();
            dbSet.Remove(entity);
        }
    }
}
