using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Abstract;
using Store.Business.Validation.Category;
using Store.DAL.DTO.Category;
using System.Text;

namespace Store.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet("GetListCategory")]
		public async Task<ActionResult<List<GetCategoryListDto>>> GetCategoryList()
		{
			var list = new List<string>();

			try
			{
				var categoryList = await _categoryService.GetCategoryListAsync();
				if (categoryList.Count == 0)
				{
					list.Add("İçerik bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				return Ok(categoryList);
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		[HttpPost("AddCategory")]
		public async Task<ActionResult<string>> AddCategory(AddCategoryDto addCategoryDto)
		{
			var list = new List<string>();
			var Validator = new AddCategoryValidator();
			var validationResult = Validator.Validate(addCategoryDto);

			if (!validationResult.IsValid )
			{
                foreach (var error in validationResult.Errors)
                {
					list.Add(error.ErrorMessage);
                }
				return Ok(new {code = StatusCode(1002),message = list,type="error"});
            }

			try
			{
				var results = await _categoryService.AddCategoryAsync(addCategoryDto);
                if (results > 0)
                {
					list.Add("Ekleme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });

				}
				else
				{
					list.Add("Ekleme işlemi başarısız");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);	
			}
		}

		[HttpGet("GetCategoryById/{id}")]
		public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
		{
			var list = new List<string>();
			if (id <= 0)
			{
				list.Add("Kategori id geçersiz.");
				return Ok(new { code = StatusCode(1001), message = list, type = "error" });
			}
			try
			{
				var result = await _categoryService.GetCategoryByIdAsync(id);
				if (result == null)
				{
					list.Add("Kategori bulunamadı.");
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

		[HttpPut("UpdateCategory")]
		public async Task<ActionResult<string>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			var list = new List<string>();
			var Validator = new UpdateCategoryValidator();
			var validationResults = Validator.Validate(updateCategoryDto);

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
				var result = await _categoryService.UpdateCategoryAsync(updateCategoryDto);
				if (result > 0)
				{
					list.Add("Güncelleme İşlemi Başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Kategori bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error " });
				}
				else
				{
					list.Add("Güncelleme İşlemi Başarısız.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error " });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("DeleteCategory/{id}")]
		public async Task<ActionResult<string>> DeleteCategory(int id)
		{
			var list = new List<string>();
			try
			{
				var result = await _categoryService.DeleteCategoryAsync(id);
				if (result > 0)
				{
					list.Add("Silme İşlemi Başarılı.");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Kategori bulunamadı.");
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
