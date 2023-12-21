using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IgorBryt.Store.DAL.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private const int PageSize = 12;
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<int> GetCountAsync(ProductPagingOptions options)
    {
        var count = options.CategoryId == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(x => x.ProductCategoryId == options.CategoryId);

        return count / PageSize + 1;
    }

    public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync(ProductPagingOptions options)
    {
        return await _dbSet
                .Skip(((options.Page ?? 1) - 1) * PageSize)
                .Take(PageSize)
                .Include(p => p.Category)
                .ToListAsync();
    }

    public async Task<Product?> GetProductWithDetailsByIdAsync(int id)
    {
        return await _dbSet
               .Include(p => p.Category)
               .FirstOrDefaultAsync(p => p.Id == id);
    }
}