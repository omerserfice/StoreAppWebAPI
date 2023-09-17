using Microsoft.EntityFrameworkCore;
using Store.Business.Abstract;
using Store.DAL.Context;
using Store.DAL.DTO.User;
using Store.DAL.Entities;
using Store.DAL.LoginSecurity.Entity;
using Store.DAL.LoginSecurity.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly StoreAppDbContext _context;
		private readonly ITokenHelper _tokenHelper;

		public AuthService(StoreAppDbContext context, ITokenHelper tokenHelper)
		{
			_context = context;
			_tokenHelper = tokenHelper;
		}

		public async Task<AccessToken> CreateAccessTokenAsync(User user)
		{
			var currentUserRoles = await GetUserRolesByUserIdAsync(user.Id);
			return currentUserRoles == null ? null : _tokenHelper.CreateToken(user, currentUserRoles);
		}

		private async Task<IEnumerable<Role>> GetUserRolesByUserIdAsync(int userId)
		{
			return await _context.UserRoles.Where(p => !p.IsDeleted && p.UserId == userId)
				.Include(p => p.RoleFK)
				.Select(p => p.RoleFK)
				.ToListAsync();
		}
		public async Task<User> GetLoginUserAsync(UserLoginDto userLoginDto)
		{
			var currentUser = await _context.Users
			   .Where(p => !p.IsDeleted && p.Email == userLoginDto.Email).FirstOrDefaultAsync();
			if (currentUser == null) return null;

			var passwordMatchResult = HashingHelper.VerifyPasswordHash(userLoginDto.Password, currentUser.PasswordHash,
				currentUser.PasswordSalt);
			if (passwordMatchResult)
			{
				return currentUser;
			}
			else
			{
				return new User();
			}
		}

		public async Task<int> RegisterAsync(UserRegisterDto userRegisterDto)
		{
			var currentTime = DateTime.Now;
			HashingHelper.CreatePasswordHash(userRegisterDto.Password, out var passwordHash, out var passwordSalt);
			var user = new User
			{
				Name = userRegisterDto.Name,
				Surname = userRegisterDto.Surname,
				Email = userRegisterDto.Email,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				CDate = currentTime,
				UserRoles = new List<UserRole>
				{
					new() {RoleId = userRegisterDto.UserRole, CDate = currentTime}
				}
			};
			await _context.Users.AddAsync(user);
			return await _context.SaveChangesAsync();
		}

		
	}
}
