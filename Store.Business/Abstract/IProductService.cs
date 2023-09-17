using Store.DAL.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Abstract
{
	public interface IProductService
	{
		Task<List<GetProductListDto>> GetProductListAsync();
		Task<GetProductDto> GetProductByIdAsync(int productId);
		Task<List<GetProductListDto>> GetProductsByCategoryIdAsync(int categoryId);
		Task<int> AddProductAsync(AddProductDto addProductDto);
		Task<int> UpdateProductAsync(UpdateProductDto updateProductDto);
		Task<int> DeletedProductAsync(int productId);
		
	}
}
