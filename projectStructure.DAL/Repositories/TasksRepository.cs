using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL
{
    public class TasksRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly ThreadContext context;
        public TasksRepository()
        {
            context = ThreadContext.getInstance();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return context.tasks.ToList() as List<TEntity>;
        }
        public virtual TasksDAL Get(int id)
        {
            return context.tasks.Where(x => x.Id == id).FirstOrDefault();
        }
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            var task = (entity as TasksDAL);
            task.Id = context.tasks.Count();
            context.tasks.Add(task);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            var p = entity as TasksDAL;
            context.tasks[p.Id] = p;
        }
        public virtual void Delete(int id)
        {
            var entity = context.tasks.Where(x => x.Id == id).FirstOrDefault();
            context.tasks.Remove(entity);
        }
    }
}
