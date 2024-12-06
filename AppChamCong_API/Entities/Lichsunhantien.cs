using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Lichsunhantien
{
    public int Idlsnt { get; set; }

    public string? Ten { get; set; }

    public double? Sotien { get; set; }

    public int? Idnguontien { get; set; }

    public DateOnly? Ngaynhan { get; set; }

    public string? Ghichu { get; set; }

    public int? Idcv { get; set; }

    public virtual Congviec? IdcvNavigation { get; set; }

    public virtual Nguontienthuvao? IdnguontienNavigation { get; set; }
}
