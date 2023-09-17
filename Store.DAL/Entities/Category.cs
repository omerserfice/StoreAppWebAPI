using AppCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
	public class Category : Audit, IEntity, ISoftDeleted
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
		public bool IsDeleted { get; set; }

	}
}
