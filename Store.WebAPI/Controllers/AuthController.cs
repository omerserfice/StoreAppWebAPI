using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Abstract;
using Store.DAL.DTO.User;

namespace Store.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
		{
			var registerResult = await _authService.RegisterAsync(userRegisterDto);
			if (registerResult > 0)
			{
				return Ok(new { code = StatusCode(1000), message = "Kullanıcı kaydı başarılı", type = "success" });
			}
			return Ok(new { code = StatusCode(1001), message = "Kullanıcı kaydı başarısız", type = "error" });
		}

		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult> Login(UserLoginDto userLoginDto)
		{
			var currentUser = await _authService.GetLoginUserAsync(userLoginDto);

			if (currentUser == null)
			{
				Ok(new { code = StatusCode(1001), message = "Kullanıcı bulunamadı", type = "error" });
			}
			else if (currentUser.Surname == null)
			{
				Ok(new { code = StatusCode(1001), message = "Kullanıcı adı veya şifre yanlış", type = "error" });
			}
			var accessToken = await _authService.CreateAccessTokenAsync(currentUser);
			return Ok(accessToken);
		}

		[Authorize]
		[HttpGet]
		[Route("GetUserName")]
		public string GetUserName()
		{
			return "Ömer"; 
		}

	}
}
