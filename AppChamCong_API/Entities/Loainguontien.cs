using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Loainguontien
{
    public int Idloainguontien { get; set; }

    public string? Tenloaint { get; set; }

    public virtual ICollection<Nguontienthuvao> Nguontienthuvaos { get; set; } = new List<Nguontienthuvao>();
}
