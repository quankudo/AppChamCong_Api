using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppChamCong_API.Entities;

public partial class QuanLyChamCongContext : DbContext
{
    public QuanLyChamCongContext()
    {
    }

    public QuanLyChamCongContext(DbContextOptions<QuanLyChamCongContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bangluong> Bangluongs { get; set; }

    public virtual DbSet<Calamviec> Calamviecs { get; set; }

    public virtual DbSet<Chamcong> Chamcongs { get; set; }

    public virtual DbSet<Chitietcongviec> Chitietcongviecs { get; set; }

    public virtual DbSet<Congviec> Congviecs { get; set; }

    public virtual DbSet<Hinhthucchamcong> Hinhthucchamcongs { get; set; }

    public virtual DbSet<Khoanchi> Khoanchis { get; set; }

    public virtual DbSet<Lichsunhantien> Lichsunhantiens { get; set; }

    public virtual DbSet<Loaikhoanchi> Loaikhoanchis { get; set; }

    public virtual DbSet<Loainguontien> Loainguontiens { get; set; }

    public virtual DbSet<Loaiphucap> Loaiphucaps { get; set; }

    public virtual DbSet<Loaitrutien> Loaitrutiens { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Nguontienthuvao> Nguontienthuvaos { get; set; }

    public virtual DbSet<Phucap> Phucaps { get; set; }

    public virtual DbSet<Quyentaomoichucnang> Quyentaomoichucnangs { get; set; }

    public virtual DbSet<Tangca> Tangcas { get; set; }

    public virtual DbSet<Trutien> Trutiens { get; set; }

    public virtual DbSet<Ungluong> Ungluongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-OLG5KKTO\\MAY1;Initial Catalog=QuanLyChamCong;User ID=sa;Password=quan26012004;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bangluong>(entity =>
        {
            entity.HasKey(e => e.Idbangluong).HasName("PK__BANGLUON__05C4F2116CD5FFD2");

            entity.ToTable("BANGLUONG", tb => tb.HasTrigger("trg_UpdateTongLuongNhanVien"));

            entity.Property(e => e.Idbangluong).HasColumnName("IDBANGLUONG");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Thoigian).HasColumnName("THOIGIAN");
            entity.Property(e => e.Tongluong).HasColumnName("TONGLUONG");
            entity.Property(e => e.Tongluongnhanduoc)
                .HasComputedColumnSql("(((isnull([TONGLUONG],(0))+isnull([TONGTIENTANGCA],(0)))-isnull([TONGTIENPHUCAP],(0)))-isnull([TONGTT],(0)))", false)
                .HasColumnName("TONGLUONGNHANDUOC");
            entity.Property(e => e.Tongtienphucap).HasColumnName("TONGTIENPHUCAP");
            entity.Property(e => e.Tongtientangca).HasColumnName("TONGTIENTANGCA");
            entity.Property(e => e.Tongtienung).HasColumnName("TONGTIENUNG");
            entity.Property(e => e.Tongtt).HasColumnName("TONGTT");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Bangluongs)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__BANGLUONG__IDCTC__6E01572D");
        });

        modelBuilder.Entity<Calamviec>(entity =>
        {
            entity.HasKey(e => e.Idca).HasName("PK__CALAMVIE__B87D80A098714E32");

            entity.ToTable("CALAMVIEC");

            entity.Property(e => e.Idca).HasColumnName("IDCA");
            entity.Property(e => e.Tenca)
                .HasMaxLength(30)
                .HasColumnName("TENCA");
            entity.Property(e => e.Tgianbatdau).HasColumnName("TGIANBATDAU");
            entity.Property(e => e.Tgianketthuc).HasColumnName("TGIANKETTHUC");

            entity.HasMany(d => d.Idcvs).WithMany(p => p.Idcas)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitietcalv",
                    r => r.HasOne<Congviec>().WithMany()
                        .HasForeignKey("Idcv")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETCAL__IDCV__4E88ABD4"),
                    l => l.HasOne<Calamviec>().WithMany()
                        .HasForeignKey("Idca")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETCAL__IDCA__4D94879B"),
                    j =>
                    {
                        j.HasKey("Idca", "Idcv").HasName("PK__CHITIETC__13FA58A870E026CF");
                        j.ToTable("CHITIETCALV");
                        j.IndexerProperty<int>("Idca").HasColumnName("IDCA");
                        j.IndexerProperty<int>("Idcv").HasColumnName("IDCV");
                    });
        });

        modelBuilder.Entity<Chamcong>(entity =>
        {
            entity.HasKey(e => e.Idcc).HasName("PK__CHAMCONG__B87D80A68E4E647E");

            entity.ToTable("CHAMCONG", tb =>
                {
                    tb.HasTrigger("trg_UpdateBangLuongNhanVien");
                    tb.HasTrigger("trg_UpdateLuongNhanVien");
                });

            entity.Property(e => e.Idcc).HasColumnName("IDCC");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Luong).HasColumnName("LUONG");
            entity.Property(e => e.Lydo)
                .HasColumnType("ntext")
                .HasColumnName("LYDO");
            entity.Property(e => e.Ngaychamcong).HasColumnName("NGAYCHAMCONG");
            entity.Property(e => e.Nghiphep).HasColumnName("NGHIPHEP");
            entity.Property(e => e.Thoigianbatdauchamcong).HasColumnName("THOIGIANBATDAUCHAMCONG");
            entity.Property(e => e.Thoigianketthucchamcong).HasColumnName("THOIGIANKETTHUCCHAMCONG");
            entity.Property(e => e.Tinhcong)
                .HasMaxLength(30)
                .HasColumnName("TINHCONG");
            entity.Property(e => e.Tongthoigianlamviec).HasColumnName("TONGTHOIGIANLAMVIEC");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(30)
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Chamcongs)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__CHAMCONG__IDCTCV__70DDC3D8");
        });

        modelBuilder.Entity<Chitietcongviec>(entity =>
        {
            entity.HasKey(e => e.Idctcv).HasName("PK__CHITIETC__0F87982206EA82C6");

            entity.ToTable("CHITIETCONGVIEC");

            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Idcv).HasColumnName("IDCV");
            entity.Property(e => e.Idnhanvien).HasColumnName("IDNHANVIEN");
            entity.Property(e => e.Tiencong).HasColumnName("TIENCONG");

            entity.HasOne(d => d.IdcvNavigation).WithMany(p => p.Chitietcongviecs)
                .HasForeignKey(d => d.Idcv)
                .HasConstraintName("FK__CHITIETCON__IDCV__52593CB8");

            entity.HasOne(d => d.IdnhanvienNavigation).WithMany(p => p.Chitietcongviecs)
                .HasForeignKey(d => d.Idnhanvien)
                .HasConstraintName("FK__CHITIETCO__IDNHA__5165187F");
        });

        modelBuilder.Entity<Congviec>(entity =>
        {
            entity.HasKey(e => e.Idcv).HasName("PK__CONGVIEC__B87D808BAB1EF243");

            entity.ToTable("CONGVIEC");

            entity.Property(e => e.Idcv).HasColumnName("IDCV");
            entity.Property(e => e.Idchunhom).HasColumnName("IDCHUNHOM");
            entity.Property(e => e.Idhtcc).HasColumnName("IDHTCC");
            entity.Property(e => e.IsGroup).HasDefaultValue(false);
            entity.Property(e => e.Maqrcode)
                .HasColumnType("text")
                .HasColumnName("MAQRCODE");
            entity.Property(e => e.Mawifichamcong)
                .HasColumnType("text")
                .HasColumnName("MAWIFICHAMCONG");
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngaychotluong).HasColumnName("NGAYCHOTLUONG");
            entity.Property(e => e.Phuongthucchamcong)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("PHUONGTHUCCHAMCONG");
            entity.Property(e => e.Tencv)
                .HasMaxLength(40)
                .HasColumnName("TENCV");
            entity.Property(e => e.Tgiannhacnhochamcong).HasColumnName("TGIANNHACNHOCHAMCONG");
            entity.Property(e => e.Tgiantretoida).HasColumnName("TGIANTRETOIDA");
            entity.Property(e => e.Thoigianketthuclamviec).HasColumnName("THOIGIANKETTHUCLAMVIEC");
            entity.Property(e => e.Thoigianlamviec).HasColumnName("THOIGIANLAMVIEC");
            entity.Property(e => e.Thongbao).HasColumnName("THONGBAO");
            entity.Property(e => e.Tongchi).HasColumnName("TONGCHI");
            entity.Property(e => e.Tongloinhuan)
                .HasComputedColumnSql("(([TONGTHU]-[TONGCHI])-[TONGLUONGNHANVIEN])", false)
                .HasColumnName("TONGLOINHUAN");
            entity.Property(e => e.Tongluongnhanvien).HasColumnName("TONGLUONGNHANVIEN");
            entity.Property(e => e.Tongthu).HasColumnName("TONGTHU");

            entity.HasOne(d => d.IdchunhomNavigation).WithMany(p => p.Congviecs)
                .HasForeignKey(d => d.Idchunhom)
                .HasConstraintName("FK__CONGVIEC__IDCHUN__4222D4EF");

            entity.HasOne(d => d.IdhtccNavigation).WithMany(p => p.Congviecs)
                .HasForeignKey(d => d.Idhtcc)
                .HasConstraintName("FK__CONGVIEC__IDHTCC__4316F928");
        });

        modelBuilder.Entity<Hinhthucchamcong>(entity =>
        {
            entity.HasKey(e => e.Idhtcc).HasName("PK__HINHTHUC__9DCF2D2F2A64AFDB");

            entity.ToTable("HINHTHUCCHAMCONG");

            entity.Property(e => e.Idhtcc).HasColumnName("IDHTCC");
            entity.Property(e => e.Tenhtcc)
                .HasMaxLength(30)
                .HasColumnName("TENHTCC");
        });

        modelBuilder.Entity<Khoanchi>(entity =>
        {
            entity.HasKey(e => e.Idkhoanchi).HasName("PK__KHOANCHI__C139C37FBBDBC4AD");

            entity.ToTable("KHOANCHI", tb => tb.HasTrigger("trg_UpdateTongChi"));

            entity.Property(e => e.Idkhoanchi).HasColumnName("IDKHOANCHI");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("text")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idcv).HasColumnName("IDCV");
            entity.Property(e => e.Idloaikhoanchi).HasColumnName("IDLOAIKHOANCHI");
            entity.Property(e => e.Ngaychi).HasColumnName("NGAYCHI");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");
            entity.Property(e => e.Tenkhoanchi)
                .HasMaxLength(30)
                .HasColumnName("TENKHOANCHI");

            entity.HasOne(d => d.IdcvNavigation).WithMany(p => p.Khoanchis)
                .HasForeignKey(d => d.Idcv)
                .HasConstraintName("FK__KHOANCHI__IDCV__59FA5E80");

            entity.HasOne(d => d.IdloaikhoanchiNavigation).WithMany(p => p.Khoanchis)
                .HasForeignKey(d => d.Idloaikhoanchi)
                .HasConstraintName("FK__KHOANCHI__IDLOAI__59063A47");
        });

        modelBuilder.Entity<Lichsunhantien>(entity =>
        {
            entity.HasKey(e => e.Idlsnt).HasName("PK__LICHSUNH__027633DF1BD6FBE9");

            entity.ToTable("LICHSUNHANTIEN", tb => tb.HasTrigger("trg_UpdateTongThu"));

            entity.Property(e => e.Idlsnt).HasColumnName("IDLSNT");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Idcv).HasColumnName("IDCV");
            entity.Property(e => e.Idnguontien).HasColumnName("IDNGUONTIEN");
            entity.Property(e => e.Ngaynhan).HasColumnName("NGAYNHAN");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");
            entity.Property(e => e.Ten)
                .HasMaxLength(30)
                .HasColumnName("TEN");

            entity.HasOne(d => d.IdcvNavigation).WithMany(p => p.Lichsunhantiens)
                .HasForeignKey(d => d.Idcv)
                .HasConstraintName("FK__LICHSUNHAN__IDCV__5629CD9C");

            entity.HasOne(d => d.IdnguontienNavigation).WithMany(p => p.Lichsunhantiens)
                .HasForeignKey(d => d.Idnguontien)
                .HasConstraintName("FK__LICHSUNHA__IDNGU__5535A963");
        });

        modelBuilder.Entity<Loaikhoanchi>(entity =>
        {
            entity.HasKey(e => e.Idloaikhoanchi).HasName("PK__LOAIKHOA__1307AC9E7744E6DB");

            entity.ToTable("LOAIKHOANCHI");

            entity.Property(e => e.Idloaikhoanchi).HasColumnName("IDLOAIKHOANCHI");
            entity.Property(e => e.Tenloaikhoanchi)
                .HasMaxLength(30)
                .HasColumnName("TENLOAIKHOANCHI");
        });

        modelBuilder.Entity<Loainguontien>(entity =>
        {
            entity.HasKey(e => e.Idloainguontien).HasName("PK__LOAINGUO__B9DFE0522A3A21BF");

            entity.ToTable("LOAINGUONTIEN");

            entity.Property(e => e.Idloainguontien).HasColumnName("IDLOAINGUONTIEN");
            entity.Property(e => e.Tenloaint)
                .HasMaxLength(30)
                .HasColumnName("TENLOAINT");
        });

        modelBuilder.Entity<Loaiphucap>(entity =>
        {
            entity.HasKey(e => e.Idloaipc).HasName("PK__LOAIPHUC__1B1E42E90C1DFDB7");

            entity.ToTable("LOAIPHUCAP");

            entity.Property(e => e.Idloaipc).HasColumnName("IDLOAIPC");
            entity.Property(e => e.Tenloaipc)
                .HasMaxLength(30)
                .HasColumnName("TENLOAIPC");
        });

        modelBuilder.Entity<Loaitrutien>(entity =>
        {
            entity.HasKey(e => e.Idloaitt).HasName("PK__LOAITRUT__1B1EE077A65B8064");

            entity.ToTable("LOAITRUTIEN");

            entity.Property(e => e.Idloaitt).HasColumnName("IDLOAITT");
            entity.Property(e => e.Tenloaitt)
                .HasMaxLength(30)
                .HasColumnName("TENLOAITT");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__NGUOIDUN__94F7C0590B2FA09B");

            entity.ToTable("NGUOIDUNG");

            entity.Property(e => e.Iduser).HasColumnName("IDUSER");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hovaten)
                .HasMaxLength(50)
                .HasColumnName("HOVATEN");
            entity.Property(e => e.Passwords)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORDS");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Trangthai).HasColumnName("TRANGTHAI");
        });

        modelBuilder.Entity<Nguontienthuvao>(entity =>
        {
            entity.HasKey(e => e.Idnguontien).HasName("PK__NGUONTIE__AF167783B8911D9E");

            entity.ToTable("NGUONTIENTHUVAO");

            entity.Property(e => e.Idnguontien).HasColumnName("IDNGUONTIEN");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("text")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idloainguontien).HasColumnName("IDLOAINGUONTIEN");
            entity.Property(e => e.Ngaytao).HasColumnName("NGAYTAO");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");
            entity.Property(e => e.Tennguontien)
                .HasMaxLength(30)
                .HasColumnName("TENNGUONTIEN");

            entity.HasOne(d => d.IdloainguontienNavigation).WithMany(p => p.Nguontienthuvaos)
                .HasForeignKey(d => d.Idloainguontien)
                .HasConstraintName("FK__NGUONTIEN__HINHA__3B75D760");
        });

        modelBuilder.Entity<Phucap>(entity =>
        {
            entity.HasKey(e => e.Idpc).HasName("PK__PHUCAP__B87C5B0CEBF29039");

            entity.ToTable("PHUCAP", tb => tb.HasTrigger("trg_UpdateTongPhuCap"));

            entity.Property(e => e.Idpc).HasColumnName("IDPC");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Idloaipc).HasColumnName("IDLOAIPC");
            entity.Property(e => e.Ngaytao).HasColumnName("NGAYTAO");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Phucaps)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__PHUCAP__IDCTCV__619B8048");

            entity.HasOne(d => d.IdloaipcNavigation).WithMany(p => p.Phucaps)
                .HasForeignKey(d => d.Idloaipc)
                .HasConstraintName("FK__PHUCAP__IDLOAIPC__628FA481");
        });

        modelBuilder.Entity<Quyentaomoichucnang>(entity =>
        {
            entity.HasKey(e => e.Idqtmcn).HasName("PK__QUYENTAO__46A44D1B355FD83C");

            entity.ToTable("QUYENTAOMOICHUCNANG");

            entity.Property(e => e.Idqtmcn).HasColumnName("IDQTMCN");
            entity.Property(e => e.Tenqtmcn)
                .HasMaxLength(30)
                .HasColumnName("TENQTMCN");

            entity.HasMany(d => d.Idcvs).WithMany(p => p.Idqtmcns)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitietquyen",
                    r => r.HasOne<Congviec>().WithMany()
                        .HasForeignKey("Idcv")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETQUY__IDCV__48CFD27E"),
                    l => l.HasOne<Quyentaomoichucnang>().WithMany()
                        .HasForeignKey("Idqtmcn")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CHITIETQU__IDQTM__47DBAE45"),
                    j =>
                    {
                        j.HasKey("Idqtmcn", "Idcv").HasName("PK__CHITIETQ__ED239513A4459D4F");
                        j.ToTable("CHITIETQUYEN");
                        j.IndexerProperty<int>("Idqtmcn").HasColumnName("IDQTMCN");
                        j.IndexerProperty<int>("Idcv").HasColumnName("IDCV");
                    });
        });

        modelBuilder.Entity<Tangca>(entity =>
        {
            entity.HasKey(e => e.Idtangca).HasName("PK__TANGCA__812A4487B1535AA7");

            entity.ToTable("TANGCA");

            entity.Property(e => e.Idtangca).HasColumnName("IDTANGCA");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Heso).HasColumnName("HESO");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("text")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Ngaytangca).HasColumnName("NGAYTANGCA");
            entity.Property(e => e.Sophut).HasColumnName("SOPHUT");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Tangcas)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__TANGCA__IDCTCV__5CD6CB2B");
        });

        modelBuilder.Entity<Trutien>(entity =>
        {
            entity.HasKey(e => e.Idtt).HasName("PK__TRUTIEN__B87C3AF8343F32B0");

            entity.ToTable("TRUTIEN", tb => tb.HasTrigger("trg_UpdateTongTruTien"));

            entity.Property(e => e.Idtt).HasColumnName("IDTT");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Idloaitt).HasColumnName("IDLOAITT");
            entity.Property(e => e.Ngaytao).HasColumnName("NGAYTAO");
            entity.Property(e => e.Sotien).HasColumnName("SOTIEN");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Trutiens)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__TRUTIEN__IDCTCV__6754599E");

            entity.HasOne(d => d.IdloaittNavigation).WithMany(p => p.Trutiens)
                .HasForeignKey(d => d.Idloaitt)
                .HasConstraintName("FK__TRUTIEN__IDLOAIT__68487DD7");
        });

        modelBuilder.Entity<Ungluong>(entity =>
        {
            entity.HasKey(e => e.Idungluong).HasName("PK__UNGLUONG__7DA3B6F2EDAD9128");

            entity.ToTable("UNGLUONG", tb => tb.HasTrigger("trg_UpdateTongUngTien"));

            entity.Property(e => e.Idungluong).HasColumnName("IDUNGLUONG");
            entity.Property(e => e.Ghichu)
                .HasColumnType("ntext")
                .HasColumnName("GHICHU");
            entity.Property(e => e.Hinhanh)
                .HasColumnType("text")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Idctcv).HasColumnName("IDCTCV");
            entity.Property(e => e.Ngayung).HasColumnName("NGAYUNG");
            entity.Property(e => e.Sotienung).HasColumnName("SOTIENUNG");

            entity.HasOne(d => d.IdctcvNavigation).WithMany(p => p.Ungluongs)
                .HasForeignKey(d => d.Idctcv)
                .HasConstraintName("FK__UNGLUONG__IDCTCV__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
