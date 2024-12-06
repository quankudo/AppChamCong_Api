using System.ComponentModel.DataAnnotations;

namespace AppChamCong_API.Dto.Request
{
    public class OvertimeReq
    {
        [Required]
        public int Idtangca { get; set; }

        public double? Heso { get; set; }

        public double? Sotien { get; set; }

        public TimeOnly? Sophut { get; set; }

        [Required]
        public DateOnly? Ngaytangca { get; set; }

        public string? Hinhanh { get; set; }

        [MaxLength(100)]
        public string? Ghichu { get; set; }

        public int? Idctcv { get; set; }
    }
}
