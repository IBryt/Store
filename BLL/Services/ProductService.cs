using AutoMapper;
using Business.Services.Abstract;
using FluentValidation;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;
using IgorBryt.Store.DAL.Interfaces;

namespace IgorBryt.Store.BLL.Services
{
    public class ProductService : BaseCrud<ProductModel, Product>, IProductService
    {
        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<ProductModel> validator,
            IRepository<Product> repository)
            : base(unitOfWork, mapper, validator, repository) { }

        public Task<int> GetCountAsync()
        {
            return _unitOfWork.ProductRepository.GetCountAsync();
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync(ProductPagingOptions options)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsWithDetailsAsync(options);
            return products
                .Select(x => _mapper.Map<ProductModel>(x))
                .AsEnumerable();
        }
    }
}
