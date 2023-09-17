using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.DTO.Product
{
	public class GetProductDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Price { get; set; }
		public string ProductDetail { get; set; }
        public string CategoryName { get; set; }
    }
}
