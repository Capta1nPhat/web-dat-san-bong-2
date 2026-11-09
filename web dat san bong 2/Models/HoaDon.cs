using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public int MaDatSan { get; set; }

    public DateTime NgayXuat { get; set; }

    public decimal TongTien { get; set; }

    public virtual DonDatSan MaDatSanNavigation { get; set; } = null!;
}
