using System.ComponentModel.DataAnnotations;

namespace AppChamCong_API.Dto.Request
{
    public class SalaryAdvanceReq
    {
        public int Idungluong { get; set; }
        [Required]
        public double? Sotienung { get; set; }
        [Required]
        public DateOnly? Ngayung { get; set; }
        [MaxLength(100)]
        public string? Ghichu { get; set; }

        public string? Hinhanh { get; set; }
        [Required]
        public int? Idctcv { get; set; }
    }
}
