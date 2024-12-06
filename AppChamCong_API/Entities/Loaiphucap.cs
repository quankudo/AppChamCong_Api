using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Loaiphucap
{
    public int Idloaipc { get; set; }

    public string? Tenloaipc { get; set; }

    public virtual ICollection<Phucap> Phucaps { get; set; } = new List<Phucap>();
}
