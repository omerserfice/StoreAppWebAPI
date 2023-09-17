using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.DTO.User
{
	public class UserRegisterDto
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
		public int UserRole { get; set; }
	}
}
