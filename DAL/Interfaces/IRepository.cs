using IgorBryt.Store.DAL.Entities.Abstract;

namespace IgorBryt.Store.DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id);

    Task AddAsync(TEntity entity);

    void Delete(TEntity entity);

    void Update(TEntity entity);
}
