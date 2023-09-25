using Microsoft.AspNetCore.Identity;
using Store.DAL.DTO.User;


namespace Store.Business.Abstract
{
	public interface IAuthenticationService
	{
		Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto); 
		Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto); 
		Task<string> CreateToken(); 
	}
}
