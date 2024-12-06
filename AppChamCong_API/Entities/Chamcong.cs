using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Chamcong
{
    public int Idcc { get; set; }

    public TimeOnly? Thoigianbatdauchamcong { get; set; }

    public TimeOnly? Thoigianketthucchamcong { get; set; }

    public DateOnly? Ngaychamcong { get; set; }

    public int? Idctcv { get; set; }

    public string? Tinhcong { get; set; }

    public string? Trangthai { get; set; }

    public bool? Nghiphep { get; set; }

    public string? Lydo { get; set; }

    public string? Ghichu { get; set; }

    public TimeOnly? Tongthoigianlamviec { get; set; }

    public double? Luong { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }
}
