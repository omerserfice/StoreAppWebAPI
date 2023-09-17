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
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(p => p.Id);
			builder.HasAlternateKey(p => p.Email);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Surname).HasMaxLength(100).IsRequired();
			builder.Property(p => p.PasswordHash).IsRequired();
			builder.Property(p => p.PasswordSalt).IsRequired();
		}
	}
}
