using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Repositories;

public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}
