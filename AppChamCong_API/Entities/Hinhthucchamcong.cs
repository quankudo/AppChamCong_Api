using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Hinhthucchamcong
{
    public int Idhtcc { get; set; }

    public string? Tenhtcc { get; set; }

    public virtual ICollection<Congviec> Congviecs { get; set; } = new List<Congviec>();
}
