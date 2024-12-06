using System.ComponentModel.DataAnnotations;

namespace AppChamCong_API.Dto.Request
{
    public class UpdateUser
    {
        [Required]
        public int id { get; set; }

        [MaxLength(100)]
        [MinLength(10)]
        public string HoTen { get; set; }
        [MaxLength(14)]
        [MinLength(10)]
        public string SDT { get; set; }
    }
}
