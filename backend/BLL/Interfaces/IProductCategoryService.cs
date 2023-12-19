using IgorBryt.Store.BLL.Models;

namespace IgorBryt.Store.BLL.Interfaces;

public interface IProductCategoryService : ICrud<ProductCategoryModel>
{
    Task<IEnumerable<ProductCategoryModel>> GetAllAsync();
}
