using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_dat_san_bong_2.Models;

public partial class NhanVien
{
    [Key]
    public string MaNV { get; set; } = null!; // Khóa chính bắt buộc có

    // Thêm dấu ? để cho phép rỗng và làm mất đường gạch xanh
    public string? TenNV { get; set; }

    public string? SoDienThoai { get; set; }

    public string? MatKhau { get; set; }

    public string? VaiTro { get; set; }

    // Chỉ giữ lại liên kết với Phân công công việc (Xóa bỏ liên kết NguoiDung và SanTheThao)
    public virtual ICollection<PhanCongNV> PhanCongNVs { get; set; } = new List<PhanCongNV>();
}


