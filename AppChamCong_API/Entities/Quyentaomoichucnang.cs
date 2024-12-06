using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Quyentaomoichucnang
{
    public int Idqtmcn { get; set; }

    public string? Tenqtmcn { get; set; }

    public virtual ICollection<Congviec> Idcvs { get; set; } = new List<Congviec>();
}
