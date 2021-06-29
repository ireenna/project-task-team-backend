using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

namespace projectStructure.DAL
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:List<Entity>
    {
        protected readonly ThreadContext context;
        public Repository()
        {
            context = ThreadContext.getInstance();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return context.projects.ToList() as List<TEntity>;
        }
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            context.projects.Add(entity as Project);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            context.projects.Add(entity as Project);
        }
        public virtual void Delete(int id)
        {
            var entity = context.projects.Where(x=>x.Id == id);
            Delete(entity as TEntity);
        }
        public virtual void Delete(TEntity entity)
        {
            var dbSet = context.projects;
            dbSet.Remove(entity as Project);
        }
    }
}
