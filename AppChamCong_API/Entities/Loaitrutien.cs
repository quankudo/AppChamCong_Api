using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Loaitrutien
{
    public int Idloaitt { get; set; }

    public string? Tenloaitt { get; set; }

    public virtual ICollection<Trutien> Trutiens { get; set; } = new List<Trutien>();
}
