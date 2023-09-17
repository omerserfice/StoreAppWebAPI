using AppCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
	public class UserRole : Audit, IEntity, ISoftDeleted
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public  User UserFK { get; set; }
        public  int  RoleId { get; set; }
        public Role RoleFK { get; set; }
        public bool IsDeleted { get; set; }
	}
}
