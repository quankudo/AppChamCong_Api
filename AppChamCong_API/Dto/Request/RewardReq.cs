using System.ComponentModel.DataAnnotations;

namespace AppChamCong_API.Dto.Request
{
    public class RewardReq
    {
        [Required]
        public int Idpc { get; set; }

        [Required]
        public int? Idctcv { get; set; }

        [Required]
        public int? Idloaipc { get; set; }

        [Required]
        public double? Sotien { get; set; }
        [Required]
        public DateOnly? Ngaytao { get; set; }

        [MaxLength(100)]
        public string? Ghichu { get; set; }
    }
}
