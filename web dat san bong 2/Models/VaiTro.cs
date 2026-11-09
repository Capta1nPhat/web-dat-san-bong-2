using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class VaiTro
{
    public int MaVaiTro { get; set; }

    public string TenVaiTro { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<NguoiDung> MaNguoiDungs { get; set; } = new List<NguoiDung>();
}
