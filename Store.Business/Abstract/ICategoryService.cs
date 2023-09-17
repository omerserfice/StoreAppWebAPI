using Store.DAL.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Abstract
{
	public interface ICategoryService
	{
		Task<List<GetCategoryListDto>> GetCategoryListAsync();
		Task<GetCategoryDto> GetCategoryByIdAsync(int categoryId);
		Task<int> AddCategoryAsync(AddCategoryDto addCategoryDto);
		Task<int> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
		Task<int> DeleteCategoryAsync(int categoryId);
	}
}
