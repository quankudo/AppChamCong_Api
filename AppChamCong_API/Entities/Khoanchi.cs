using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Khoanchi
{
    public int Idkhoanchi { get; set; }

    public int? Idloaikhoanchi { get; set; }

    public string? Tenkhoanchi { get; set; }

    public double? Sotien { get; set; }

    public DateOnly? Ngaychi { get; set; }

    public string? Hinhanh { get; set; }

    public int? Idcv { get; set; }

    public virtual Congviec? IdcvNavigation { get; set; }

    public virtual Loaikhoanchi? IdloaikhoanchiNavigation { get; set; }
}
