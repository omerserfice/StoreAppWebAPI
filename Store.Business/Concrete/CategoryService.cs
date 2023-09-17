using Microsoft.EntityFrameworkCore;
using Store.Business.Abstract;
using Store.DAL.Context;
using Store.DAL.DTO.Category;
using Store.DAL.Entities;
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

		public CategoryService(StoreAppDbContext context)
		{
			_context = context;
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
				return -1;
			}
			category.IsDeleted = true;
			_context.Categories.Update(category);
			return await _context.SaveChangesAsync();	
		}

		public async Task<GetCategoryDto> GetCategoryByIdAsync(int categoryId)
		{
			return await _context.Categories.Where(p => !p.IsDeleted && p.Id == categoryId)
				.Select(p => new GetCategoryDto
				{
					Id = p.Id,
					Name = p.Name,
				}).FirstOrDefaultAsync();

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
			if(category == null) { return -1; }
			category.Name = updateCategoryDto.Name;
			_context.Categories.Update(category);
			return await _context.SaveChangesAsync();
		}
	}
}
