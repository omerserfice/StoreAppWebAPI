using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DAL.Entities;
using System.Reflection;


namespace Store.DAL.Context
{
	public class StoreAppDbContext : IdentityDbContext<User>
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=StoreDb;Trusted_Connection=True;");
		}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
	
	}
}
