using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.DAL.Entities;
using Store.DAL.LoginSecurity.Encryption;
using Store.DAL.LoginSecurity.Entity;
using Store.DAL.LoginSecurity.Extension;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.LoginSecurity.Helper
{
	public class TokenHelper : ITokenHelper
	{
		private readonly TokenOptions _tokenOptions;
		private DateTime _accessTokenExpiration;
		//IOptions => Microsoft.Extensions.Options
		public TokenHelper(IOptions<TokenOptions> tokenOptions)
		{
			_tokenOptions = tokenOptions.Value;
		}

		public AccessToken CreateToken(User user, IEnumerable<Role> userRoles)
		{
			_accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
			var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
			var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, userRoles);
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
			return new AccessToken
			{
				Token = token,
				Expiration = _accessTokenExpiration
			};
		}

		private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
		   SigningCredentials signingCredentials, IEnumerable<Role> roles)
		{
			return new JwtSecurityToken(
				tokenOptions.Issuer,
				tokenOptions.Audience,
				SetJwtClaims(user, roles),
				DateTime.Now,
				_accessTokenExpiration,
				signingCredentials);
		}

		private static IEnumerable<Claim> SetJwtClaims(User user, IEnumerable<Role> roles)
		{
			var claims = new List<Claim>();
			claims.AddUserIdentifier(user.Id);
			claims.AddFullName($"{user.Name} {user.Surname}");
			claims.AddRole(roles.Select(p => p.Name).AsEnumerable());
			return claims;
		}
	}
}
