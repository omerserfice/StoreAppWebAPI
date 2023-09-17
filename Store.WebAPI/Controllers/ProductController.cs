using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Abstract;
using Store.Business.Validation.Product;
using Store.DAL.DTO.Product;

namespace Store.WebAPI.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
	
		[HttpGet("GetProductList")]
		public async Task<ActionResult<List<GetProductListDto>>> GetProductList()
		{
			try
			{
				var result = await _productService.GetProductListAsync();
				return Ok(new { code = StatusCode(1000), message = result, type = "success" });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetProductById/{id}")]
		public async Task<ActionResult<GetProductDto>> GetProductById(int id)
		{
			var list = new List<string>();
			if (id <= 0)
			{
				list.Add("Ürün Id geçersiz.");
				return Ok(new { code = StatusCode(1001), message = list, type = "error" });
			}
			try
			{
				var result = await _productService.GetProductByIdAsync(id);
				if (result == null)
				{
					list.Add("Ürün bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					return Ok(new { code = StatusCode(1000), message = result, type = "success" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetProductByCategoryId/{categoryId}")]
		public async Task<ActionResult<List<GetProductListDto>>> GetProductByCategoryId(int categoryId)
		{
			var list = new List<string>();
			if (categoryId <= 0)
			{
				list.Add("Ürün Id geçersiz.");
				return Ok(new { code = StatusCode(1001), message = list, type = "error" });
			}
			try
			{
				var result = await _productService.GetProductsByCategoryIdAsync(categoryId);
				if (result == null)
				{
					list.Add("Ürün bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					return Ok(new { code = StatusCode(1000), message = result, type = "success" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AddProduct")]
		public async Task<ActionResult<string>> AddProduct(AddProductDto addProductDto)
		{
			var list = new List<string>();
			var validator = new AddProductValidator();
			var validationResults = validator.Validate(addProductDto);
			if (!validationResults.IsValid)
			{
				foreach (var error in validationResults.Errors)
				{
					list.Add(error.ErrorMessage);
				}
				return Ok(new { code = StatusCode(1002), message = list, type = "error" });
			}
			try
			{
				var result = await _productService.AddProductAsync(addProductDto);
				if (result > 0)
				{
					list.Add("Ekleme İşlemi Başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else
				{
					list.Add("Ekleme İşlemi Başarısız.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPut("UpdateProduct")]
		public async Task<ActionResult<string>> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var list = new List<string>();
			var validator = new UpdateProductValidator();
			var validationResults = validator.Validate(updateProductDto);

			if (!validationResults.IsValid)
			{
				foreach (var error in validationResults.Errors)
				{
					list.Add(error.ErrorMessage);
				}
				return Ok(new { code = StatusCode(1002), message = list, type = "error" });
			}
			try
			{
				var result = await _productService.UpdateProductAsync(updateProductDto);
				if (result > 0)
				{
					list.Add("Güncelleme İşlemi Başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Ürün bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Güncelleme İşlemi Başarısız.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("DeleteProduct/{id}")]
		public async Task<ActionResult<string>> DeleteProduct(int id)
		{
			var list = new List<string>();
			try
			{
				var result = await _productService.DeletedProductAsync(id);
				if (result > 0)
				{
					list.Add("Silme İşlemi Başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Ürün bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Silme İşlemi Başarısız.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
