using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL.Repositories
{
    public class TeamRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly ThreadContext context;
        public TeamRepository()
        {
            context = ThreadContext.getInstance();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            return context.teams.ToList() as List<TEntity>;
        }
        public virtual TeamDAL Get(int id)
        {
            return context.teams.Where(x => x.Id == id).FirstOrDefault();
        }
        public virtual void Create(TEntity entity, string createdBy = null)
        {
            var team = (entity as TeamDAL);
            team.Id = context.teams.Count();
            context.teams.Add(team);
        }
        public virtual void Update(TEntity entity, string updatedBy = null)
        {
            var p = entity as TeamDAL;
            context.teams[p.Id] = p;
        }
        public virtual void Delete(int id)
        {
            var entity = context.teams.Where(x => x.Id == id).FirstOrDefault();
            context.teams.Remove(entity);
        }
    }
}
