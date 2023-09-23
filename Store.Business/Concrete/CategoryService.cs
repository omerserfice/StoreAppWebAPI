using Microsoft.EntityFrameworkCore;
using Store.Business.Abstract;
using Store.DAL.Context;
using Store.DAL.DTO.Category;
using Store.DAL.Entities;
using Store.DAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly StoreAppDbContext _context;
		private readonly ILoggerService _logger;

		public CategoryService(StoreAppDbContext context, ILoggerService logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<int> AddCategoryAsync(AddCategoryDto addCategoryDto)
		{
			var newCategory = new Category
			{
				Name = addCategoryDto.Name,
			};
			await _context.Categories.AddAsync(newCategory);
			return await _context.SaveChangesAsync();
		}

		public async Task<int> DeleteCategoryAsync(int categoryId)
		{
			var category = await _context.Categories.Where(p => !p.IsDeleted && p.Id == categoryId).FirstOrDefaultAsync();
			if (category == null)
			{
				//string message = $"{categoryId} id ile kayıtlı bir kategori bulunamadı.";
				//{
				//	_logger.LogInfo(message);
				//}

				throw new CategoryNotFoundException(categoryId);

				
			}
			category.IsDeleted = true;
			_context.Categories.Update(category);
			return await _context.SaveChangesAsync();	
		}

		public async Task<GetCategoryDto> GetCategoryByIdAsync(int categoryId)
		{
			 var category = _context.Categories.Where(p => !p.IsDeleted && p.Id == categoryId)
				.Select(p => new GetCategoryDto
				{
					Id = p.Id,
					Name = p.Name,
				}).FirstOrDefaultAsync();

			if (category is null)
			
				throw new CategoryNotFoundException(categoryId);
			
			return await category;
		}

		public async Task<List<GetCategoryListDto>> GetCategoryListAsync()
		{
			return await _context.Categories.Where(p => !p.IsDeleted)
				.Select(p => new GetCategoryListDto
				{
					Id = p.Id,
					Name = p.Name,
				}).ToListAsync(); ;
		}

		public async Task<int> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
		{
			var category = await _context.Categories.Where(p => !p.IsDeleted && p.Id == updateCategoryDto.Id).FirstOrDefaultAsync();
			if(category == null) {
				//string message = $"{updateCategoryDto.Id} id ile kayıtlı bir kategori bulunamadı.";
				//_logger.LogInfo(message);
				throw new CategoryNotFoundException(updateCategoryDto.Id);
				
			}
			category.Name = updateCategoryDto.Name;
			_context.Categories.Update(category);
			return await _context.SaveChangesAsync();
		}
	}
}
