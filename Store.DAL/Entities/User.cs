using AppCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
	public class User : Audit, IEntity, ISoftDeleted
	{
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public bool IsDeleted { get; set; }
	}
}
