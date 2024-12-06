using System.ComponentModel.DataAnnotations;

namespace AppChamCong_API.Dto.Request
{
    public class UserRegister
    {
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirmPassword { get; set; }
    }
}
