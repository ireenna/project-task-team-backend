using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.Models;

namespace projectStructure.DAL
{
    public class ProjectRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly ThreadContext context;
        public ProjectRepository()
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
            var entity = context.projects.Where(x => x.Id == id);
            var dbSet = context.projects;
            dbSet.Remove(entity as Project);
        }
    }
}
