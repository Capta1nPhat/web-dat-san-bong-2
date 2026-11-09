using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace web_dat_san_bong_2.Models;

public partial class QuanLyDatSanContext : DbContext
{
    public QuanLyDatSanContext()
    {
    }

    public QuanLyDatSanContext(DbContextOptions<QuanLyDatSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BangGium> BangGia { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DiemTichLuy> DiemTichLuys { get; set; }

    public virtual DbSet<DonDatSan> DonDatSans { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichHoatDong> LichHoatDongs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

   
    public virtual DbSet<PhanCongNV> PhanCongNVs { get; set; }

    public virtual DbSet<SanTheThao> SanTheThaos { get; set; }

    public virtual DbSet<SanYeuThich> SanYeuThiches { get; set; }

    

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<TienIch> TienIches { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-57FE1FRH\\SQLEXPRESS;Database=QuanLyDatSan;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BangGium>(entity =>
        {
            entity.HasKey(e => e.MaGia).HasName("PK__BangGia__3CD3DE5E8EC40071");

            entity.Property(e => e.MaGia).ValueGeneratedNever();
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaSanNavigation).WithMany(p => p.BangGia)
                .HasForeignKey(d => d.MaSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BangGia__MaSan__74AE54BC");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGia__AA9515BFA86122E1");

            entity.Property(e => e.MaDanhGia).ValueGeneratedNever();
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NhanXet).HasMaxLength(255);

            entity.HasOne(d => d.MaDatSanNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaDatSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaDatSa__0E6E26BF");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaNguoi__0F624AF8");
        });

        modelBuilder.Entity<DiemTichLuy>(entity =>
        {
            entity.HasKey(e => e.MaDiem).HasName("PK__DiemTich__33326025218AF602");

            entity.ToTable("DiemTichLuy");

            entity.HasIndex(e => e.MaNguoiDung, "UQ__DiemTich__C539D76332702C04").IsUnique();

            entity.Property(e => e.MaDiem).ValueGeneratedNever();

            entity.HasOne(d => d.MaNguoiDungNavigation).WithOne(p => p.DiemTichLuy)
                .HasForeignKey<DiemTichLuy>(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemTichL__MaNgu__151B244E");
        });

        modelBuilder.Entity<DonDatSan>(entity =>
        {
            entity.HasKey(e => e.MaDatSan).HasName("PK__DonDatSa__747DC2D3FDBED5F6");

            entity.ToTable("DonDatSan");

            // MaDatSan is an identity column in the database; configure EF to
            // generate the value on add so EF does not try to insert an explicit
            // value (which causes IDENTITY_INSERT errors).
            entity.Property(e => e.MaDatSan).ValueGeneratedOnAdd();
            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThaiDon)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xác nhận");

            entity.HasOne(d => d.MaGiaNavigation).WithMany(p => p.DonDatSans)
                .HasForeignKey(d => d.MaGia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonDatSan__MaGia__7C4F7684");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.DonDatSans)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK__DonDatSan__MaKM__7D439ABD");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DonDatSans)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonDatSan__MaNgu__7A672E12");

            entity.HasOne(d => d.MaSanNavigation).WithMany(p => p.DonDatSans)
                .HasForeignKey(d => d.MaSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonDatSan__MaSan__7B5B524B");

            entity.HasOne(d => d.MaTienIchNavigation).WithMany(p => p.DonDatSans)
                .HasForeignKey(d => d.MaTienIch)
                .HasConstraintName("FK__DonDatSan__MaTie__7E37BEF6");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B9B8A76BF");

            entity.ToTable("HoaDon");

            entity.HasIndex(e => e.MaDatSan, "UQ__HoaDon__747DC2D2C301F774").IsUnique();

            entity.Property(e => e.MaHoaDon).ValueGeneratedNever();
            entity.Property(e => e.NgayXuat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MaDatSanNavigation).WithOne(p => p.HoaDon)
                .HasForeignKey<HoaDon>(d => d.MaDatSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaDatSan__09A971A2");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF15B43FEADA");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKm)
                .ValueGeneratedNever()
                .HasColumnName("MaKM");
            entity.Property(e => e.GiaTriGiam).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenKm)
                .HasMaxLength(100)
                .HasColumnName("TenKM");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Hoạt động");
        });

        modelBuilder.Entity<LichHoatDong>(entity =>
        {
            entity.HasKey(e => e.MaLich).HasName("PK__LichHoat__728A9AE9B9703F4C");

            entity.ToTable("LichHoatDong");

            entity.Property(e => e.MaLich).ValueGeneratedNever();

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.LichHoatDongs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHoatD__MaNgu__60A75C0F");

            entity.HasOne(d => d.MaSanNavigation).WithMany(p => p.LichHoatDongs)
                .HasForeignKey(d => d.MaSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHoatD__MaSan__619B8048");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D7629E47E0B1");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.SoDienThoai, "UQ__NguoiDun__0389B7BDA171B081").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105347D92AD53").IsUnique();

            entity.Property(e => e.MaNguoiDung).ValueGeneratedOnAdd();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NgonNgu)
                .HasMaxLength(50)
                .HasDefaultValue("Tiếng Việt");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Hoạt động");

            entity.HasMany(d => d.MaVaiTros).WithMany(p => p.MaNguoiDungs)
                .UsingEntity<Dictionary<string, object>>(
                    "PhanQuyen",
                    r => r.HasOne<VaiTro>().WithMany()
                        .HasForeignKey("MaVaiTro")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhanQuyen__MaVai__5535A963"),
                    l => l.HasOne<NguoiDung>().WithMany()
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhanQuyen__MaNgu__5441852A"),
                    j =>
                    {
                        j.HasKey("MaNguoiDung", "MaVaiTro").HasName("PK__PhanQuye__291D137E9F21E4E6");
                        j.ToTable("PhanQuyen");
                    });
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNV); // MaNV là khóa chính
            entity.ToTable("NHANVIEN"); // Tên bảng trong SQL

            // Khai báo các cột
            entity.Property(e => e.MaNV).HasMaxLength(50);
            entity.Property(e => e.TenNV).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.VaiTro).HasMaxLength(50);
        });



        modelBuilder.Entity<PhanCongNV>(entity =>
        {
            entity.HasKey(e => e.MaPC); // MaPC là khóa chính
            entity.ToTable("PHANCONGNV"); // Tên bảng trong SQL

           
            
        });

        modelBuilder.Entity<SanTheThao>(entity =>
        {
            entity.HasKey(e => e.MaSan).HasName("PK__SanTheTh__3188C8269E44A08E");

            entity.ToTable("SanTheThao");

            entity.Property(e => e.MaSan).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LoaiSan).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenSan).HasMaxLength(100);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Hoạt động");

            entity.HasOne(d => d.MaChuSanNavigation).WithMany(p => p.SanTheThaos)
                .HasForeignKey(d => d.MaChuSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanTheTha__MaChu__5CD6CB2B");
        });

        modelBuilder.Entity<SanYeuThich>(entity =>
        {
            entity.HasKey(e => e.MaYeuThich).HasName("PK__SanYeuTh__B9007E4CFC1BC111");

            entity.ToTable("SanYeuThich");

            entity.HasIndex(e => new { e.MaNguoiDung, e.MaSan }, "UQ__SanYeuTh__B6215BE1089BE9D2").IsUnique();

            entity.Property(e => e.MaYeuThich).ValueGeneratedNever();

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.SanYeuThiches)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanYeuThi__MaNgu__19DFD96B");

            entity.HasOne(d => d.MaSanNavigation).WithMany(p => p.SanYeuThiches)
                .HasForeignKey(d => d.MaSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanYeuThi__MaSan__1AD3FDA4");
        });

       

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B25844B43A929A");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.MaThanhToan).ValueGeneratedNever();
            entity.Property(e => e.LoaiThanhToan).HasMaxLength(50);
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.PhuongThuc).HasMaxLength(50);
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chưa thanh toán")
                .HasColumnName("trangThai");

            entity.HasOne(d => d.MaDatSanNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaDatSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThanhToan__MaDat__02FC7413");
        });

        modelBuilder.Entity<TienIch>(entity =>
        {
            entity.HasKey(e => e.MaTienIch).HasName("PK__TienIch__4697D8EA5BA2AA12");

            entity.ToTable("TienIch");

            entity.Property(e => e.MaTienIch).ValueGeneratedNever();
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenTienIch).HasMaxLength(100);

            entity.HasOne(d => d.MaSanNavigation).WithMany(p => p.TienIches)
                .HasForeignKey(d => d.MaSan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TienIch__MaSan__656C112C");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CF90B06884");

            entity.ToTable("VaiTro");

            entity.HasIndex(e => e.TenVaiTro, "UQ__VaiTro__1DA55814518049AE").IsUnique();

            entity.Property(e => e.MaVaiTro).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
