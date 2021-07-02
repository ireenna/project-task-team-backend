using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.DAL;

namespace projectStructure.DAL.Repositories
{
    public class FullProjectRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly ThreadContext context;
        public FullProjectRepository()
        {
            context = ThreadContext.getInstance();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return context.fullprojects.ToList() as List<TEntity>;
        }
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            context.fullprojects.Add(entity as FullProjectsDAL);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            context.fullprojects.Add(entity as FullProjectsDAL);
        }
        public virtual void Delete(int id)
        {
            var entity = context.fullprojects.Where(x => x.Id == id);
            var dbSet = context.fullprojects;
            dbSet.Remove(entity as FullProjectsDAL);
        }
    }
}
