using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_dat_san_bong_2.Models;

public partial class DonDatSan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaDatSan { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaSan { get; set; }

    public int MaGia { get; set; }

    public int? MaKm { get; set; }

    public int? MaTienIch { get; set; }

    public DateTime NgayDat { get; set; }

    public DateOnly NgayChoi { get; set; }
    public TimeOnly? GioBatDau { get; set; }

    public TimeOnly? GioKetThuc { get; set; }

    public string TrangThaiDon { get; set; } = null!;

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual HoaDon? HoaDon { get; set; }

    public virtual BangGium MaGiaNavigation { get; set; } = null!;

    public virtual KhuyenMai? MaKmNavigation { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual SanTheThao MaSanNavigation { get; set; } = null!;

    public virtual TienIch? MaTienIchNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
