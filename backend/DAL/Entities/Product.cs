using IgorBryt.Store.DAL.Entities.Abstract;

namespace IgorBryt.Store.DAL.Entities;

public class Product : BaseEntity
{
    public int ProductCategoryId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public ProductCategory? Category { get; set; }
}
