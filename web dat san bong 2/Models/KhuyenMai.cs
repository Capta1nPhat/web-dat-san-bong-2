using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class KhuyenMai
{
    public int MaKm { get; set; }

    public string TenKm { get; set; } = null!;

    public decimal GiaTriGiam { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<DonDatSan> DonDatSans { get; set; } = new List<DonDatSan>();
}
