using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Ungluong
{
    public int Idungluong { get; set; }

    public double? Sotienung { get; set; }

    public DateOnly? Ngayung { get; set; }

    public string? Ghichu { get; set; }

    public string? Hinhanh { get; set; }

    public int? Idctcv { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }
}
