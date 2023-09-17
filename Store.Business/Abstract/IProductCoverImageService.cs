using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Abstract
{
	public interface IProductCoverImageService
	{
		Task<byte[]> Get(int productId);
		Task<int> Add(int productId, IFormFile file);
		Task<int> Update(int productId, IFormFile file);
		Task<int> Delete(int productId);
    }
}
