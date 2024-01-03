using IgorBryt.Store.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IgorBryt.Store.DAL.Configuration;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.CategoryName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);
    }
}
