using Microsoft.AspNetCore.Identity;


namespace Store.DAL.Entities
{
	public class User : IdentityUser
	{
       
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
		//public string? Password { get; set; }
		//public string? Email { get; set; }
		//public string? PhoneNumber { get; set; }
	
	}
}
