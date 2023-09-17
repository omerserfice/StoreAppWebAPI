using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Config
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
		  builder.HasKey(x => x.Id);
		  builder.Property(x => x.Id).ValueGeneratedOnAdd();
		  builder.Property(x=>x.Name).HasMaxLength(200).IsRequired();
		  builder.Property(x=>x.ProductDetail).HasMaxLength(1000).IsRequired();
		  builder.Property(x=>x.Price).HasMaxLength(200).IsRequired();
		  builder.Property(x => x.CategoryId).IsRequired();
		  builder.HasOne(x=>x.CategoryFK).WithMany(x => x.Products).HasForeignKey(x=>x.CategoryId);
		}
	}
}
