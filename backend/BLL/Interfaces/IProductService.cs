﻿using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Data.Common;
using IgorBryt.Store.DAL.Entities;

namespace IgorBryt.Store.BLL.Interfaces;

public interface IProductService : ICrud<ProductModel>
{
    Task<int> GetCountPageAsync(FilterProductModel filter);
    Task<IEnumerable<ProductModel>> GetProductsAsync(FilterProductModel filter);
    Task<ProductModel?> GetProductWithDetailsByIdAsync(int id);
    Task<IEnumerable<ProductModel>> GetProductWithDetailsByIdsAsync(int[] ids);
}