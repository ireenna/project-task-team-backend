using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

namespace projectStructure.DAL
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        void Create(TEntity entity, string createdBy = null);
        void Update(TEntity entity, string updatedBy = null);
        void Delete(int id);
        void Delete(TEntity entity);
    }
}
