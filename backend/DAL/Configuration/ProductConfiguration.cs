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
            .IsUnicode()
            .HasMaxLength(250);

        builder.Property(p => p.Description)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(1000);

        builder.Property(p => p.ImageUrl)
           .IsUnicode(false)
           .HasMaxLength(50);

        builder.Property(p => p.Price)
            .IsRequired();
    }
}
