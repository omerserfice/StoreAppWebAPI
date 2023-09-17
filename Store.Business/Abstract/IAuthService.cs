using Store.DAL.DTO.User;
using Store.DAL.Entities;
using Store.DAL.LoginSecurity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Abstract
{
	public interface IAuthService 
	{
		Task<AccessToken> CreateAccessTokenAsync(User user);
		Task<User> GetLoginUserAsync(UserLoginDto userLoginDto);
		Task<int> RegisterAsync(UserRegisterDto userRegisterDto);
	}
}
