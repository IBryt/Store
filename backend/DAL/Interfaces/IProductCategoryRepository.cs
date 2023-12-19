using IgorBryt.Store.DAL.Entities;

namespace IgorBryt.Store.DAL.Interfaces;

public interface IProductCategoryRepository : IRepository<ProductCategory>
{
    Task<IEnumerable<ProductCategory>> GetAllAsync();
}
