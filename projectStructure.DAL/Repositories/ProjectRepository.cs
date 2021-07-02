using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ProjectDAL Get(int id)
        {
            var a= context.projects.Where(x=>x.Id == id).FirstOrDefault();
            return a;
        }
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            var project = (entity as ProjectDAL);
            project.Id = context.projects.Count();
            context.projects.Add(project);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            var p = entity as ProjectDAL;
            context.projects[p.Id] = entity as ProjectDAL;
        }
        public virtual void Delete(int id)
        {
            var entity = context.projects.Where(x => x.Id == id).FirstOrDefault();
            context.projects.Remove(entity);
        }
    }
}
