using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class DanhGium
{
    public int MaDanhGia { get; set; }

    public int MaDatSan { get; set; }

    public int MaNguoiDung { get; set; }

    public int SoSao { get; set; }

    public string? NhanXet { get; set; }

    public DateTime NgayDanhGia { get; set; }

    public virtual DonDatSan MaDatSanNavigation { get; set; } = null!;

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
