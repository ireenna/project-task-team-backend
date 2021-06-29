using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

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
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            context.tasks.Add(entity as Tasks);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            context.tasks.Add(entity as Tasks);
        }
        public virtual void Delete(int id)
        {
            var entity = context.tasks.Where(x => x.Id == id);
            var dbSet = context.tasks;
            dbSet.Remove(entity as Tasks);
        }
    }
}
