
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Store.Business.Abstract;
using Store.DAL.DTO.User;

namespace Store.WebAPI.Controllers
{
	[Route("api/authentication")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _service;

		public AuthenticationController(IAuthenticationService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterUser([FromBody]UserForRegistirationDto userForRegistirationDto)
		{
		

			var result = await _service.RegisterUser(userForRegistirationDto);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Authenticate([FromBody]UserForAuthenticationDto user)
		{
			if (!await _service.ValidateUser(user))
				return Unauthorized();
			return Ok(new
			{
				Token = await _service.CreateToken()
			});
		}

	}
}
