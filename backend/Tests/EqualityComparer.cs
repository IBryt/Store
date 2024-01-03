using IgorBryt.Store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorBryt.Store.Tests;

internal class EqualityComparer
{
    internal class ProductEqualityComparer : IEqualityComparer<Product>
    {
        public bool Equals([AllowNull] Product x, [AllowNull] Product y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.ProductCategoryId == y.ProductCategoryId
                && x.ProductName == y.ProductName
                && x.Price == y.Price
                && x.ImageUrl == y.ImageUrl
                && x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ProductCategoryEqualityComparer : IEqualityComparer<ProductCategory>
    {
        public bool Equals([AllowNull] ProductCategory x, [AllowNull] ProductCategory y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.CategoryName == y.CategoryName;
        }

        public int GetHashCode([DisallowNull] ProductCategory obj)
        {
            return obj.GetHashCode();
        }
    }
}
