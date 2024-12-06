using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Calamviec
{
    public int Idca { get; set; }

    public string? Tenca { get; set; }

    public TimeOnly? Tgianbatdau { get; set; }

    public TimeOnly? Tgianketthuc { get; set; }

    public virtual ICollection<Congviec> Idcvs { get; set; } = new List<Congviec>();
}
