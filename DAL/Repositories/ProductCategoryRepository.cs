using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories.Abstract;

namespace IgorBryt.Store.DAL.Repositories;

public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext dbContext) : base(dbContext) { }

    public Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
