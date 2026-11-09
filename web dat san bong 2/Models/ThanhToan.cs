using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class ThanhToan
{
    public int MaThanhToan { get; set; }

    public int MaDatSan { get; set; }

    public string PhuongThuc { get; set; } = null!;

    public decimal SoTien { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayThanhToan { get; set; }

    public string LoaiThanhToan { get; set; } = null!;

    public virtual DonDatSan MaDatSanNavigation { get; set; } = null!;
}
