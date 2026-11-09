using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_dat_san_bong_2.Models;

namespace web_dat_san_bong_2.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly QuanLyDatSanContext _context;

        public ThanhToanController(QuanLyDatSanContext context)
        {
            _context = context;
        }

        //==========================================
        // TRANG THANH TOÁN
        //==========================================

        public IActionResult Index(int id)
        {
            var don = _context.DonDatSans
                .Include(x => x.MaSanNavigation)
                .Include(x => x.MaGiaNavigation)
                .Include(x => x.MaKmNavigation)
                .FirstOrDefault(x => x.MaDatSan == id);

            if (don == null)
                return NotFound();

            decimal tongTien = TinhTien(don);

            ViewBag.TongTien = tongTien;

            return View(don);
        }

        //==========================================
        // TÍNH TIỀN
        //==========================================

        private decimal TinhTien(DonDatSan don)
        {
            decimal gia1Gio = don.MaGiaNavigation.Gia;

            //--------------------------------------------------
            // Nếu chưa có giờ thì lấy mặc định 1 giờ
            //--------------------------------------------------

            if (!don.GioBatDau.HasValue || !don.GioKetThuc.HasValue)
            {
                return gia1Gio;
            }

            TimeSpan thoiLuong =
                don.GioKetThuc.Value - don.GioBatDau.Value;

            if (thoiLuong.TotalHours <= 0)
            {
                thoiLuong = TimeSpan.FromHours(1);
            }

            decimal tongTien =
                gia1Gio * (decimal)thoiLuong.TotalHours;

            //--------------------------------------------------
            // Phụ thu cuối tuần
            //--------------------------------------------------

            DateTime ngayChoi =
                don.NgayChoi.ToDateTime(TimeOnly.MinValue);

            if (ngayChoi.DayOfWeek == DayOfWeek.Saturday ||
                ngayChoi.DayOfWeek == DayOfWeek.Sunday)
            {
                tongTien *= 1.20m;
            }

            //--------------------------------------------------
            // Phụ thu buổi tối
            //--------------------------------------------------

            if (don.GioBatDau.Value.Hour >= 18)
            {
                tongTien *= 1.10m;
            }

            //--------------------------------------------------
            // Khuyến mãi
            //--------------------------------------------------

            if (don.MaKmNavigation != null)
            {
                tongTien -= don.MaKmNavigation.GiaTriGiam;
            }

            if (tongTien < 0)
                tongTien = 0;

            return decimal.Round(tongTien, 0);
        }

        //==========================================
        // QR CODE
        //==========================================

        public IActionResult QRCode(int id, string phuongThuc)
        {
            var don = _context.DonDatSans

                .Include(x => x.MaGiaNavigation)

                .Include(x => x.MaKmNavigation)

                .Include(x => x.MaSanNavigation)

                .FirstOrDefault(x => x.MaDatSan == id);

            if (don == null)
                return NotFound();

            decimal tongTien = TinhTien(don);

            ViewBag.SoTien = tongTien;

            ViewBag.PhuongThuc = phuongThuc;

            //---------------------------------------
            // THÔNG TIN NHẬN TIỀN
            //---------------------------------------

            string bank = "970407";

            string stk = "3924062006";

            string ten = "NGUYEN HOANG NGUYEN";

            string noiDung = "Thanh toan don " + don.MaDatSan;

            ViewBag.QR =
                $"https://img.vietqr.io/image/{bank}-{stk}-compact2.png?amount={tongTien}&addInfo={noiDung}&accountName={ten}";

            return View(don);
        }
        //==========================================
        // XÁC NHẬN ĐÃ THANH TOÁN (SAU KHI QUÉT QR)
        //==========================================

        [HttpPost]
        public IActionResult XacNhanThanhToan(int maDatSan, string phuongThuc)
        {
            var don = _context.DonDatSans
                .Include(x => x.MaGiaNavigation)
                .Include(x => x.MaKmNavigation)
                .FirstOrDefault(x => x.MaDatSan == maDatSan);

            if (don == null)
                return NotFound();

            // Nếu đã thanh toán thì không tạo nữa
            if (_context.ThanhToans.Any(x => x.MaDatSan == maDatSan))
            {
                return RedirectToAction("HoaDon", new { id = maDatSan });
            }

            decimal tongTien = TinhTien(don);

            ThanhToan tt = new ThanhToan();

            // Nếu MaThanhToan không Identity
            if (_context.ThanhToans.Any())
                tt.MaThanhToan = _context.ThanhToans.Max(x => x.MaThanhToan) + 1;
            else
                tt.MaThanhToan = 1;

            tt.MaDatSan = maDatSan;
            tt.PhuongThuc = phuongThuc;
            tt.SoTien = tongTien;
            tt.TrangThai = "Đã thanh toán";
            tt.LoaiThanhToan = "Thanh toán toàn bộ";
            tt.NgayThanhToan = DateTime.Now;

            _context.ThanhToans.Add(tt);

           
            // Cập nhật trạng thái đơn
            

            don.TrangThaiDon = "Đã thanh toán";

          
            // Tạo hóa đơn
          

            HoaDon hd = new HoaDon();

            if (_context.HoaDons.Any())
                hd.MaHoaDon = _context.HoaDons.Max(x => x.MaHoaDon) + 1;
            else
                hd.MaHoaDon = 1;

            hd.MaDatSan = maDatSan;
            hd.NgayXuat = DateTime.Now;
            hd.TongTien = tongTien;

            _context.HoaDons.Add(hd);

            _context.SaveChanges();

            return RedirectToAction("HoaDon", new { id = maDatSan });
        }

        [HttpPost]
        public IActionResult ThanhToanTaiSan(int maDatSan)
        {
            return XacNhanThanhToan(maDatSan, "Thanh toán tại sân");
        }

        

        public IActionResult HoaDon(int id)
        {
            var hd = _context.HoaDons

                .Include(x => x.MaDatSanNavigation)

                .ThenInclude(x => x.MaSanNavigation)

                .Include(x => x.MaDatSanNavigation)

                .ThenInclude(x => x.MaNguoiDungNavigation)

                .FirstOrDefault(x => x.MaDatSan == id);

            if (hd == null)
                return NotFound();

            return View(hd);
        }

        

        public IActionResult LichSu()
        {
            var ds = _context.ThanhToans

                .Include(x => x.MaDatSanNavigation)

                .ThenInclude(x => x.MaSanNavigation)

                .OrderByDescending(x => x.NgayThanhToan)

                .ToList();

            return View(ds);
        }
       

        public IActionResult ChiTiet(int id)
        {
            var tt = _context.ThanhToans

                .Include(x => x.MaDatSanNavigation)
                    .ThenInclude(x => x.MaSanNavigation)

                .Include(x => x.MaDatSanNavigation)
                    .ThenInclude(x => x.MaNguoiDungNavigation)

                .FirstOrDefault(x => x.MaThanhToan == id);

            if (tt == null)
                return NotFound();

            return View(tt);
        }
    
        // TÌM KIẾM THANH TOÁN
       

        public IActionResult TimKiem(string keyword)
        {
            var ds = _context.ThanhToans

                .Include(x => x.MaDatSanNavigation)
                    .ThenInclude(x => x.MaSanNavigation)

                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                ds = ds.Where(x =>
                    x.MaThanhToan.ToString().Contains(keyword)
                    || x.PhuongThuc.Contains(keyword)
                    || x.MaDatSanNavigation.MaSanNavigation.TenSan.Contains(keyword));
            }

            return View("LichSu", ds.OrderByDescending(x => x.NgayThanhToan).ToList());
        }
     
        // LỌC PHƯƠNG THỨC
    

        public IActionResult Loc(string phuongThuc)
        {
            var ds = _context.ThanhToans

                .Include(x => x.MaDatSanNavigation)

                .ThenInclude(x => x.MaSanNavigation)

                .Where(x => x.PhuongThuc == phuongThuc)

                .OrderByDescending(x => x.NgayThanhToan)

                .ToList();

            return View("LichSu", ds);
        }
       
        // HỦY THANH TOÁN
      

        public IActionResult Huy(int id)
        {
            var tt = _context.ThanhToans
                .FirstOrDefault(x => x.MaThanhToan == id);

            if (tt == null)
                return NotFound();

            tt.TrangThai = "Đã hủy";

            var don = _context.DonDatSans
                .FirstOrDefault(x => x.MaDatSan == tt.MaDatSan);

            if (don != null)
            {
                don.TrangThaiDon = "Chưa thanh toán";
            }

            _context.SaveChanges();

            return RedirectToAction("LichSu");
        }
       
        // DOANH THU
       

        public IActionResult DoanhThu()
        {
            decimal tong = _context.ThanhToans
                .Where(x => x.TrangThai == "Hoàn thành")
                .Sum(x => x.SoTien);

            ViewBag.DoanhThu = tong;

            return View();
        }
        
        // IN HÓA ĐƠN
       

        public IActionResult InHoaDon(int id)
        {
            var hd = _context.HoaDons

                .Include(x => x.MaDatSanNavigation)
                    .ThenInclude(x => x.MaSanNavigation)

                .Include(x => x.MaDatSanNavigation)
                    .ThenInclude(x => x.MaNguoiDungNavigation)

                .FirstOrDefault(x => x.MaHoaDon == id);

            if (hd == null)
                return NotFound();

            return View(hd);
        }
    }
}