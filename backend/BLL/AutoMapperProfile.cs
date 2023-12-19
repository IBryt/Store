using AutoMapper;
using IgorBryt.Store.BLL.Models;
using IgorBryt.Store.DAL.Entities;

namespace IgorBryt.Store.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductModel>()
            .ForMember(pm => pm.CategoryName, opts => opts.MapFrom(p => p.Category != null ? p.Category.CategoryName : null))
            .ForMember(pm => pm.ProductCategoryId, opts => opts.MapFrom(p => p.ProductCategoryId))
            .ReverseMap();

        CreateMap<ProductCategory, ProductCategoryModel>()
            .ForMember(pcm => pcm.ProductIds, pc => pc.MapFrom(x => x.Products.Select(x => x.Id)))
            .ReverseMap();
    }
}