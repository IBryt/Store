using IgorBryt.Store.DAL.Interfaces;

namespace IgorBryt.Store.DAL.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public UnitOfWork(
        AppDbContext dbContext,
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
    }

    public virtual IProductRepository ProductRepository => _productRepository;

    public virtual IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository;

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _dbContext.Dispose();
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
