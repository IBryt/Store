using AutoMapper;
using Business.Services.Abstract;
using FluentValidation;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;

namespace IgorBryt.Store.BLL.Services;

public class ProductCategoryService : BaseCrud<ProductCategoryModel, ProductCategory>, IProductCategoryService
{
    public ProductCategoryService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<ProductCategoryModel> validator,
        IRepository<ProductCategory> repository)
        : base(unitOfWork, mapper, validator, repository) { }

    public async Task<IEnumerable<ProductCategoryModel>> GetAllAsync()
    {
        var categories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();
        return categories.Select(x => _mapper.Map<ProductCategoryModel>(x));
    }
}
