using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class SanTheThao
{
    public int MaSan { get; set; }

    public int MaChuSan { get; set; }

    public string TenSan { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string LoaiSan { get; set; } = null!;

    public string? MoTa { get; set; }

    public string TrangThai { get; set; } = null!;

    public TimeOnly GioMo { get; set; }

    public TimeOnly GioDong { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<BangGium> BangGia { get; set; } = new List<BangGium>();

    public virtual ICollection<DonDatSan> DonDatSans { get; set; } = new List<DonDatSan>();

    public virtual ICollection<LichHoatDong> LichHoatDongs { get; set; } = new List<LichHoatDong>();

    public virtual NguoiDung MaChuSanNavigation { get; set; } = null!;

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();

    public virtual ICollection<SanYeuThich> SanYeuThiches { get; set; } = new List<SanYeuThich>();

    public virtual ICollection<TienIch> TienIches { get; set; } = new List<TienIch>();
}
