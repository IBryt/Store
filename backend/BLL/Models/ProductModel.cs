using IgorBryt.Store.BLL.Models.Abstract;

namespace IgorBryt.Store.BLL.Models;

public class ProductModel : BaseModel
{
    public int ProductCategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
}
