using AutoMapper;
using Business.Services.Abstract;
using FluentValidation;
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
    IMapper mapper,
    IValidator<ProductModel> validator)
    : base(unitOfWork, mapper, validator, unitOfWork.ProductRepository) { }

public Task<int> GetCountAsync(FilterProductModel filter)
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
}
