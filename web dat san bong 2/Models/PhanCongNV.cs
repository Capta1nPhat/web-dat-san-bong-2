using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace web_dat_san_bong_2.Models

{
    [Table("PHANCONGNV")]
    public class PhanCongNV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Báo cho C# biết cột này tự động tăng
        public int MaPC { get; set; }

        [Required]
        public string MaNV { get; set; } = null!;

        [Required]
        public string HoTen { get; set; } = null!;

        [Required]
        public string PhongBan { get; set; } = null!;

        [Required]
        public DateTime Ngay { get; set; }

        [Required]
        public string Gio { get; set; } = null!;

        [Required]
        public string PhanCong { get; set; } = null!;

        // GhiChu cho phép NULL trong SQL, nên ở đây ta thêm dấu ?
        public string? GhiChu { get; set; }
    }
}
