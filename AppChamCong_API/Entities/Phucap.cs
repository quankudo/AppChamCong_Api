using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Phucap
{
    public int Idpc { get; set; }

    public int? Idctcv { get; set; }

    public int? Idloaipc { get; set; }

    public double? Sotien { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Ghichu { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }

    public virtual Loaiphucap? IdloaipcNavigation { get; set; }
}
