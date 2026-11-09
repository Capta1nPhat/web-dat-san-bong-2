using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class DiemTichLuy
{
    public int MaDiem { get; set; }

    public int MaNguoiDung { get; set; }

    public int TongDiem { get; set; }

    public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
}
