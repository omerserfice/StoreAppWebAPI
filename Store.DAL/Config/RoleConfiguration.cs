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
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

			builder.HasData(new Role { Id = 1, Name = "Admin" });
			builder.HasData(new Role { Id = 2, Name = "User" });
		}
	}
}
