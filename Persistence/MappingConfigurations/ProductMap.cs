using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.MappingConfigurations
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey("CategoryId");

            builder.Property(p => p.Title)
                .HasConversion(t => t.Value, v => new Title(v));

            builder.Property(p => p.Description)
                .HasConversion(d => d.Value, v => string.IsNullOrEmpty(v) ? null : new Description(v));

            builder.Property(p => p.Price)
                .HasConversion(m => m.Value, v => new Money(v));

            builder.Property(p => p.Impressions)
                .HasConversion(i => i.Value, v => new Impressions(v));

            builder.Property(p => p.Dietaries)
                .HasConversion(
                    d => d.Serialized,
                    v => new DietaryFlags(v));

            builder.Property(p => p.Image)
                .HasConversion(i => i.Url, url => string.IsNullOrEmpty(url) ? null : new Image(url));
        }
    }
}
