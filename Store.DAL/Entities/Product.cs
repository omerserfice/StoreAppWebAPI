using AppCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
	public class Product : Audit, IEntity, ISoftDeleted
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int Price { get; set; }
        public string ProductDetail { get; set; }
		
        public int CategoryId { get; set; }
        public Category CategoryFK { get; set; }
		public bool IsDeleted { get; set; }
	}
}
