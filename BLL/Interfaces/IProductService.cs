using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Data.Common;

namespace IgorBryt.Store.BLL.Interfaces;

public interface IProductService : ICrud<ProductModel>
{
    Task<int> GetCountAsync();

    Task<IEnumerable<ProductModel>> GetProductsAsync(ProductPagingOptions options);
}