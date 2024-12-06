using System;
using System.Collections.Generic;

namespace AppChamCong_API.Entities;

public partial class Congviec
{
    public int Idcv { get; set; }

    public string? Tencv { get; set; }

    public int? Idchunhom { get; set; }

    public int? Ngaychotluong { get; set; }

    public string? Phuongthucchamcong { get; set; }

    public int? Idhtcc { get; set; }

    public bool? Thongbao { get; set; }

    public TimeOnly? Thoigianlamviec { get; set; }

    public TimeOnly? Thoigianketthuclamviec { get; set; }

    public TimeOnly? Tgiannhacnhochamcong { get; set; }

    public double? Tongluongnhanvien { get; set; }

    public double? Tongthu { get; set; }

    public double? Tongchi { get; set; }

    public string? Mawifichamcong { get; set; }

    public string? Maqrcode { get; set; }

    public double? Tgiantretoida { get; set; }

    public double? Tongloinhuan { get; set; }

    public DateOnly? NgayTao { get; set; }

    public bool? IsGroup { get; set; }

    public virtual ICollection<Chitietcongviec> Chitietcongviecs { get; set; } = new List<Chitietcongviec>();

    public virtual Nguoidung? IdchunhomNavigation { get; set; }

    public virtual Hinhthucchamcong? IdhtccNavigation { get; set; }

    public virtual ICollection<Khoanchi> Khoanchis { get; set; } = new List<Khoanchi>();

    public virtual ICollection<Lichsunhantien> Lichsunhantiens { get; set; } = new List<Lichsunhantien>();

    public virtual ICollection<Calamviec> Idcas { get; set; } = new List<Calamviec>();

    public virtual ICollection<Quyentaomoichucnang> Idqtmcns { get; set; } = new List<Quyentaomoichucnang>();
}
