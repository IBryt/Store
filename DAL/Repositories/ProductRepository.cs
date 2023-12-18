using IgorBryt.Store.DAL.Data;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;
using IgorBryt.Store.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<int> GetCount()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(ProductPagingOptions options)
    {
        return await _dbSet
                .Skip((options.Page - 1) * options.PageSize)
                .Take(options.PageSize)
                .ToListAsync();
    }
}