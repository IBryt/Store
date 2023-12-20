using IgorBryt.Store.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IgorBryt.Store.DAL.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(p => p.ProductCategoryId)
            .IsRequired();

        builder.Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.ImageUrl)
           .HasMaxLength(50);

        builder.Property(p => p.Price)
            .IsRequired();
    }
}
