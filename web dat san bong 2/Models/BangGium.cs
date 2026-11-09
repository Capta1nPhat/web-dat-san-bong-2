using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class BangGium
{
    public int MaGia { get; set; }

    public int MaSan { get; set; }

    public DateOnly NgayApDung { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public decimal Gia { get; set; }

    public virtual ICollection<DonDatSan> DonDatSans { get; set; } = new List<DonDatSan>();

    public virtual SanTheThao MaSanNavigation { get; set; } = null!;
}
