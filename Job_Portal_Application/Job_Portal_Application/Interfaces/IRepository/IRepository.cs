using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IRepository<TKey, TEntity>
    {
        Task<TEntity> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<TEntity> Get(TKey id);
        Task<TEntity> Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();

    }
}
