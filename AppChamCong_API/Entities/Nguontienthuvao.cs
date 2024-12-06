using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Nguontienthuvao
{
    public int Idnguontien { get; set; }

    public string? Tennguontien { get; set; }

    public double? Sotien { get; set; }

    public int? Idloainguontien { get; set; }

    public DateOnly? Ngaytao { get; set; }

    public string? Ghichu { get; set; }

    public string? Hinhanh { get; set; }

    public virtual Loainguontien? IdloainguontienNavigation { get; set; }

    public virtual ICollection<Lichsunhantien> Lichsunhantiens { get; set; } = new List<Lichsunhantien>();
}
