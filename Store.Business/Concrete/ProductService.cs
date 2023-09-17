using Microsoft.EntityFrameworkCore;
using Store.Business.Abstract;
using Store.DAL.Context;
using Store.DAL.DTO.Product;
using Store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Concrete
{
	public class ProductService : IProductService
	{

		private readonly StoreAppDbContext _context;

		public ProductService(StoreAppDbContext context)
		{
			_context = context;
		}

		public async Task<int> AddProductAsync(AddProductDto addProductDto)
		{
			var newProduct = new Product()
			{
				Name = addProductDto.Name,
				Price = addProductDto.Price,
				ProductDetail = addProductDto.ProductDetail,
				CategoryId = addProductDto.CategoryId,
			};
			await _context.Products.AddAsync(newProduct);
			return await _context.SaveChangesAsync();

		}


		public async Task<int> DeletedProductAsync(int productId)
		{
			var product = await _context.Products.Where(p => !p.IsDeleted && p.Id == productId).FirstOrDefaultAsync();
			if (product == null) { return -1; }
			product.IsDeleted = true;
			_context.Products.Update(product);
			return await _context.SaveChangesAsync();

		}


		public async Task<GetProductDto> GetProductByIdAsync(int productId)
		{
			return await _context.Products.Where(p => !p.IsDeleted && p.Id == productId)
				.Include(p => p.CategoryFK)
				.Select(p => new GetProductDto
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					ProductDetail = p.ProductDetail,
					CategoryName = p.CategoryFK.Name
				}).FirstOrDefaultAsync();
		}

		public async Task<List<GetProductListDto>> GetProductListAsync()
		{
			return await _context.Products.Where(p => !p.IsDeleted)
				.Include(p => p.CategoryFK)
				.Select(p => new GetProductListDto
				{
					Id = p.Id,
					Name = p.Name,
					Price = p.Price,
					ProductDetail = p.ProductDetail,
					CategoryName = p.CategoryFK.Name
				}).ToListAsync();
		}

		public async Task<List<GetProductListDto>> GetProductsByCategoryIdAsync(int categoryId)
		{
			return await _context.Products.Where(p => !p.IsDeleted && p.CategoryId == categoryId).Include(p => p.CategoryFK)
			   .Select(p => new GetProductListDto
			   {
				   Name = p.Name,
				   Id = p.Id,
				   Price = p.Price,
				   ProductDetail = p.ProductDetail,
				   CategoryName = p.CategoryFK.Name,
			   }).ToListAsync();
		}

		public async Task<int> UpdateProductAsync(UpdateProductDto updateProductDto)
		{
			var product = await _context.Products.Where(p => !p.IsDeleted && p.Id == updateProductDto.Id).FirstOrDefaultAsync();
			if (product == null) { return -1; };
			product.Name = updateProductDto.Name;
			product.Price = updateProductDto.Price;
			product.ProductDetail = updateProductDto.ProductDetail;
			product.CategoryId = updateProductDto.CategoryId;
			_context.Products.Update(product);
			return await _context.SaveChangesAsync();
		}
	}
}
