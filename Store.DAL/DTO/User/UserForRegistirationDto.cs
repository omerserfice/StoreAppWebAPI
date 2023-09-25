using System.ComponentModel.DataAnnotations;

namespace Store.DAL.DTO.User
{
	public class UserForRegistirationDto 
	{
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; init; }
		[Required(ErrorMessage = "Şifre Boş Geçilemez")]
		public string? Password { get; init; }
		public string? Email { get; init; }
		public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; } // init demek tanımlandğı yerde bu ifadenin verilmesi gerektiği anlamı taşır.
    }
}
