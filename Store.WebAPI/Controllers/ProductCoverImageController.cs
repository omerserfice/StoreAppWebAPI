using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Abstract;

namespace Store.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductCoverImageController : ControllerBase
	{
		private readonly IProductCoverImageService _productCoverImageService;

		public ProductCoverImageController(IProductCoverImageService productCoverImageService)
		{
			_productCoverImageService = productCoverImageService;
		}

		[HttpGet]
		[Route("{productId:int}")]
		public async Task<ActionResult> Get(int productId)
		{
			try
			{
				var list = new List<string>();
				var result = await _productCoverImageService.Get(productId);
				if (result == null)
				{
					list.Add("Ürün resmi bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					return File(result, "image/jpeg");
				}
			}
			catch (Exception e)
			{

				return BadRequest(e.Message + e.InnerException);
			}
		}

		[HttpPost]
		[Route("{productId:int}")]
		public async Task<ActionResult> Add(int productId,IFormFile file)
		{
			var list = new List<string>();
			try
			{
				var result = await _productCoverImageService.Add(productId, file);
				if (result == 1)
				{
					list.Add("Ekleme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });

				}
				else if (result == -1)
				{
					list.Add("Bu ürüne ait resim var.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Resim yüklenirken hata oluştu.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception e)
			{

				return BadRequest(e.Message + e.InnerException);

			}
		}

		[HttpPut]
		[Route("{productId:int}")]
		public async Task<ActionResult> Update(int productId,IFormFile file)
		{
			try
			{
				var list = new List<string>();
				var result = await _productCoverImageService.Update(productId, file);
				if (result == 1)
				{
					list.Add("Güncelleme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Ürüne ait resim bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Resim güncellerken hata oluştu.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
			}
			catch (Exception e)
			{

				return BadRequest(e.Message + e.InnerException);
			}
		}

		[HttpDelete]
		[Route("{productId}")]
		public async Task<ActionResult> Delete(int productId)
		{
			try
			{
				var list = new List<string>();
				var result = await _productCoverImageService.Delete(productId);
				if (result == 1)
				{
					list.Add("Silme işlemi başarılı");
					return Ok(new { code = StatusCode(1000), message = list, type = "success" });
				}
				else if (result == -1)
				{
					list.Add("Ürüne ait resim bulunamadı.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}
				else
				{
					list.Add("Resim silinirken hata oluştu.");
					return Ok(new { code = StatusCode(1001), message = list, type = "error" });
				}

			}
			catch (Exception e)
			{

				return BadRequest(e.Message + e.InnerException);
			}
		}

	}
}
