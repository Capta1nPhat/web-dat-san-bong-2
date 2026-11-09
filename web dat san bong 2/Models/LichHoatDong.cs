using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class LichHoatDong
{
    public int MaLich { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaSan { get; set; }

    public DateOnly NgayDat { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual SanTheThao MaSanNavigation { get; set; } = null!;
}
