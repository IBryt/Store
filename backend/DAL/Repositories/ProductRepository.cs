using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private const int PageSize = 12;
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<int> GetCountPageAsync(ProductPagingOptions options)
    {
        var count = options.CategoryId == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(x => x.ProductCategoryId == options.CategoryId);

        return (count - 1) / PageSize + 1;
    }

    public async Task<IEnumerable<Product>> GetProductsWithDetailsAsync(ProductPagingOptions options)
    {
        var products = options.CategoryId == null
            ? _dbSet
            : _dbSet.Where(p => p.ProductCategoryId == options.CategoryId);

        return await products
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

    public async Task<IEnumerable<Product>> GetProductWithDetailsByIdsAsync(int[] ids)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}