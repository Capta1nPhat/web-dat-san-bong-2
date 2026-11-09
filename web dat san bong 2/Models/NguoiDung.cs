using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class NguoiDung
{
    public int MaNguoiDung { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string NgonNgu { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual DiemTichLuy? DiemTichLuy { get; set; }

    public virtual ICollection<DonDatSan> DonDatSans { get; set; } = new List<DonDatSan>();

    public virtual ICollection<LichHoatDong> LichHoatDongs { get; set; } = new List<LichHoatDong>();


   

    public virtual ICollection<SanTheThao> SanTheThaos { get; set; } = new List<SanTheThao>();

    public virtual ICollection<SanYeuThich> SanYeuThiches { get; set; } = new List<SanYeuThich>();



    public virtual ICollection<VaiTro> MaVaiTros { get; set; } = new List<VaiTro>();
}
