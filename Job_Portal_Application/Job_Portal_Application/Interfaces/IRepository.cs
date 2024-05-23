using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces
{
    public interface IRepository<TKey, TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<bool> Delete(TKey id);
        Task<TEntity> Get(TKey id);
        Task<TEntity> Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
