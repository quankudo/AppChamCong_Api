using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Chitietcongviec
{
    public int Idctcv { get; set; }

    public int? Idnhanvien { get; set; }

    public int? Idcv { get; set; }

    public double? Tiencong { get; set; }

    public virtual ICollection<Bangluong> Bangluongs { get; set; } = new List<Bangluong>();

    public virtual ICollection<Chamcong> Chamcongs { get; set; } = new List<Chamcong>();

    public virtual Congviec? IdcvNavigation { get; set; }

    public virtual Nguoidung? IdnhanvienNavigation { get; set; }

    public virtual ICollection<Phucap> Phucaps { get; set; } = new List<Phucap>();

    public virtual ICollection<Tangca> Tangcas { get; set; } = new List<Tangca>();

    public virtual ICollection<Trutien> Trutiens { get; set; } = new List<Trutien>();

    public virtual ICollection<Ungluong> Ungluongs { get; set; } = new List<Ungluong>();
}
