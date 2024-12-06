using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Nguoidung
{
    public int Iduser { get; set; }

    public string? Hovaten { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string? Passwords { get; set; }

    public bool? Trangthai { get; set; }

    public virtual ICollection<Chitietcongviec> Chitietcongviecs { get; set; } = new List<Chitietcongviec>();

    public virtual ICollection<Congviec> Congviecs { get; set; } = new List<Congviec>();
}
