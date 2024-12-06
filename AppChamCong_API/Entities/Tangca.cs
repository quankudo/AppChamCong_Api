using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Tangca
{
    public int Idtangca { get; set; }

    public double? Heso { get; set; }

    public double? Sotien { get; set; }

    public TimeOnly? Sophut { get; set; }

    public DateOnly? Ngaytangca { get; set; }

    public string? Hinhanh { get; set; }

    public string? Ghichu { get; set; }

    public int? Idctcv { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }
}
