using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL
{
    public interface IRepository<TEntity> 
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        void Create(TEntity entity, string createdBy = null);
        void Update(TEntity entity, string updatedBy = null);
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
