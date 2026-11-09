using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_dat_san_bong_2.Models
{
    [Table("ADMIN")] // Tên bảng chính xác trong SQL của bạn
    public class Admin
    {
        [Key]
        [Column("MaAD", TypeName = "varchar(10)")]
        public string MaAD { get; set; }

        [Column("TenAD", TypeName = "nvarchar(50)")]
        public string TenAD { get; set; }

        [Column("TaiKhoan", TypeName = "varchar(50)")]
        public string TaiKhoan { get; set; }

        [Column("MatKhau", TypeName = "varchar(100)")] 
        public string MatKhau { get; set; }

        [Column("VaiTro", TypeName = "varchar(10)")]
        public string VaiTro { get; set; }
    }
}