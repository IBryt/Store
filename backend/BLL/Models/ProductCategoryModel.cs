using IgorBryt.Store.BLL.Models.Abstract;

namespace IgorBryt.Store.BLL.Models;

public class ProductCategoryModel : BaseModel
{
    public string? CategoryName { get; set; }
    public ICollection<int> ProductIds { get; set; } = new List<int>();
}
