using AutoMapper;
using Business.Services.Abstract;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;

namespace IgorBryt.Store.BLL.Services;

public class ProductService : BaseCrud<ProductModel, Product>, IProductService
{
public ProductService(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : base(unitOfWork, mapper, unitOfWork.ProductRepository) { }

    public Task<int> GetCountPageAsync(FilterProductModel filter)
    {
            var options = _mapper.Map<ProductPagingOptions>(filter);
            return _unitOfWork.ProductRepository.GetCountPageAsync(options);
    }

    public async Task<IEnumerable<ProductModel>> GetProductsAsync(FilterProductModel filter)
    {
        var options = _mapper.Map<ProductPagingOptions>(filter);
        var products = await _unitOfWork.ProductRepository.GetProductsWithDetailsAsync(options);
        return products
            .Select(x => _mapper.Map<ProductModel>(x))
            .AsEnumerable();
    }

    public async Task<ProductModel?> GetProductWithDetailsByIdAsync(int id)
    {
        var entity = await _unitOfWork.ProductRepository.GetProductWithDetailsByIdAsync(id);
        if (entity == null)
        {
            throw new StoreException($"The object {typeof(Product)} cannot be null");
        }
        return _mapper.Map<ProductModel>(entity);
    }

    public async Task<IEnumerable<ProductModel>> GetProductWithDetailsByIdsAsync(int[] ids)
    {
        var products = await _unitOfWork.ProductRepository.GetProductWithDetailsByIdsAsync(ids);
        return products
           .Select(x => _mapper.Map<ProductModel>(x))
           .AsEnumerable();
    }
}
