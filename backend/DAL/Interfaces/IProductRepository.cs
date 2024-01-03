using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;

namespace IgorBryt.Store.DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithDetailsAsync(ProductPagingOptions options);
    Task<int> GetCountPageAsync(ProductPagingOptions options);
    Task<Product?> GetProductWithDetailsByIdAsync(int id);
}
