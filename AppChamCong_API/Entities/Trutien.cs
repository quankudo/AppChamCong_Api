using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Trutien
{
    public int Idtt { get; set; }

    public int? Idctcv { get; set; }

    public int? Idloaitt { get; set; }

    public double? Sotien { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Ghichu { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }

    public virtual Loaitrutien? IdloaittNavigation { get; set; }
}
