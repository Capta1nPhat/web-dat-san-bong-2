using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class SanYeuThich
{
    public int MaYeuThich { get; set; }

    public int MaNguoiDung { get; set; }

    public int MaSan { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;

    public virtual SanTheThao MaSanNavigation { get; set; } = null!;
}
