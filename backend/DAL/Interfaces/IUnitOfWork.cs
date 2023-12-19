namespace IgorBryt.Store.DAL.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }

    IProductCategoryRepository ProductCategoryRepository { get; }

    Task SaveAsync();
}
