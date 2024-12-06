using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Loaikhoanchi
{
    public int Idloaikhoanchi { get; set; }

    public string? Tenloaikhoanchi { get; set; }

    public virtual ICollection<Khoanchi> Khoanchis { get; set; } = new List<Khoanchi>();
}
