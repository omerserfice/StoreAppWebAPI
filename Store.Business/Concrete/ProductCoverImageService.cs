using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using Store.Business.Abstract;
using Store.DAL.Context;
using Store.DAL.MongoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Concrete
{
	public class ProductCoverImageService : IProductCoverImageService
	{
		private readonly IMongoCollection<ProductCoverImage> _productCoverImage;

		public ProductCoverImageService(IMongoDbContext mongoDbContext)
		{
			_productCoverImage = mongoDbContext.GetCollection<ProductCoverImage>();
		}
		public async Task<int> Add(int productId, IFormFile file)
		{
			var coverObject = await _productCoverImage.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
			if (coverObject != null)
				return -1;
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				var newCoverObject = new ProductCoverImage
				{
					Id = ObjectId.GenerateNewId(),
					ProductId = productId,
					Image = memoryStream.ToArray()
				};

				await _productCoverImage.InsertOneAsync(newCoverObject);
			}
			return 1;
			
		}

		public async Task<int> Delete(int productId)
		{
			var coverObject = await _productCoverImage.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
			if (coverObject == null) return -1;

			var result = await _productCoverImage.DeleteOneAsync(p => p.ProductId == productId);
			if (!result.IsAcknowledged) return -2;
			return 1;
		}

		public async Task<byte[]> Get(int productId)
		{
			var coverObject = await _productCoverImage.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
			if (coverObject == null) return null;
			return coverObject.Image;
			
		}

		public async Task<int> Update(int productId, IFormFile file)
		{
			var coverObject = await _productCoverImage.Find(p => p.ProductId == productId).FirstOrDefaultAsync();
			if (coverObject == null) return -1;

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				coverObject.Image = memoryStream.ToArray();

				var result = await _productCoverImage.ReplaceOneAsync(p => p.ProductId == productId, coverObject);

				if (!result.IsAcknowledged) return -2;
				return 1;
			}
		}
	}
}
