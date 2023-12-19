using IgorBryt.Store.DAL.Entities.Abstract;

namespace IgorBryt.Store.DAL.Entities;

public class ProductCategory : BaseEntity
{
    public string? CategoryName { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
