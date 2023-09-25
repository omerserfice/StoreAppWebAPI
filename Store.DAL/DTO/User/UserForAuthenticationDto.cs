
using System.ComponentModel.DataAnnotations;


namespace Store.DAL.DTO.User
{
	public record UserForAuthenticationDto
	{
		[Required(ErrorMessage ="Kullanıcı adı girilmelidir.")]
		public string? UserName { get; init; }
		[Required(ErrorMessage = "Şifre girilmelidir.")]
		public string? Password { get; init; }
	}
}
