using System;
using System.Collections.Generic;

namespace web_dat_san_bong_2.Models;

public partial class TienIch
{
    public int MaTienIch { get; set; }

    public int MaSan { get; set; }

    public string TenTienIch { get; set; } = null!;

    public decimal Gia { get; set; }

    public virtual ICollection<DonDatSan> DonDatSans { get; set; } = new List<DonDatSan>();

    public virtual SanTheThao MaSanNavigation { get; set; } = null!;
}
