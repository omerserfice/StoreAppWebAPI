using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Business.Abstract;
using Store.DAL.DTO.User;
using Store.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Business.Concrete
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly ILoggerService _logger;
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;

		private User? _user;


		public AuthenticationService(ILoggerService logger, UserManager<User> userManager, IConfiguration configuration)
		{
			_logger = logger;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<string> CreateToken()
		{
			var signingCredentials = GetSigninCredentials();
			var claims = await GetClaims();
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		

		public async Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto)
		{
			var user = new User
			{
				FirstName = userForRegistirationDto.FirstName,
				LastName = userForRegistirationDto.LastName,
				Email = userForRegistirationDto.Email,
				PhoneNumber = userForRegistirationDto.PhoneNumber,
				UserName = userForRegistirationDto.UserName,
				
				
			};

			var roles = string.Join(Environment.NewLine, userForRegistirationDto.Roles);
			var result = await _userManager.CreateAsync(user, userForRegistirationDto.Password);
			if (result.Succeeded)
				await _userManager.AddToRoleAsync(user, roles);

			return result;
			
		}
		//Kullanıcı Doğrulama
		public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
		{
			_user = await _userManager.FindByNameAsync(userForAuthenticationDto.UserName);
			var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthenticationDto.Password));

			if (!result)
			{
				_logger.LogWarning($"{nameof(ValidateUser)}: Doğrulama hatası. Kullanıcı adı ya da Şifre Yanlış");
			}
			return result;
		}

		private SigningCredentials GetSigninCredentials()
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
			var secret = new SymmetricSecurityKey(key);
			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}
		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,_user.UserName)
			};
			var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
				claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
		}
		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");

			var tokenOptions = new JwtSecurityToken(
				issuer: jwtSettings["validIssuer"],
				audience: jwtSettings["validAudience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
				signingCredentials: signingCredentials);

			return tokenOptions;
		}
	}
}
