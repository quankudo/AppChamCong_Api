using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Bangluong
{
    public int Idbangluong { get; set; }

    public DateOnly? Thoigian { get; set; }

    public double? Tongtientangca { get; set; }

    public double? Tongtienphucap { get; set; }

    public double? Tongtt { get; set; }

    public double? Tongtienung { get; set; }

    public double? Tongluong { get; set; }

    public int? Idctcv { get; set; }

    public double Tongluongnhanduoc { get; set; }

    public virtual Chitietcongviec? IdctcvNavigation { get; set; }
}
