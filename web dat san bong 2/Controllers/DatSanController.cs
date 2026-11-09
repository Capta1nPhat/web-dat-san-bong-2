using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_dat_san_bong_2.Models;

namespace web_dat_san_bong_2.Controllers
{
    public class DatSanController : Controller
    {
        private readonly QuanLyDatSanContext _context;

        public DatSanController(QuanLyDatSanContext context)
        {
            _context = context;
        }

     

        [HttpGet]
        public IActionResult Create(int id, int maGia, DateOnly ngay)
        {
            var san = _context.SanTheThaos
                .Include(x => x.BangGia)
                .FirstOrDefault(x => x.MaSan == id);

            if (san == null)
                return NotFound();

            var bangGia = _context.BangGia
                .FirstOrDefault(x => x.MaGia == maGia);

            if (bangGia == null)
                return NotFound();

            ViewBag.Ngay = ngay;
            ViewBag.MaGia = maGia;
            ViewBag.BangGia = bangGia;

            return View(san);
        }

       
        // LƯU ĐƠN ĐẶT
       

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(int maSan, int maGia, DateOnly ngay)
        {
            
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            
            var bangGia = _context.BangGia.FirstOrDefault(x => x.MaGia == maGia);
            var san = _context.SanTheThaos.FirstOrDefault(x => x.MaSan == maSan);

            if (bangGia == null || san == null) return NotFound();

           
            bool trung = _context.DonDatSans.Any(x =>
                x.MaSan == maSan &&
                x.NgayChoi == ngay &&
                x.GioBatDau == bangGia.GioBatDau &&
                x.GioKetThuc == bangGia.GioKetThuc &&
                x.TrangThaiDon != "Đã hủy");

            if (trung)
            {
                TempData["Error"] = "Khung giờ này đã có người đặt.";
                return RedirectToAction("LichDat", "SanBong", new { id = maSan, ngay = ngay });
            }

           
            
            var don = new DonDatSan
            {
                MaNguoiDung = userId.Value,
                MaSan = maSan,
                MaGia = maGia,
                NgayDat = DateTime.Now,
                NgayChoi = ngay,
                GioBatDau = bangGia.GioBatDau,
                GioKetThuc = bangGia.GioKetThuc,
                TrangThaiDon = "Chờ xác nhận",
                MaKm = null,
                MaTienIch = null
            };

            _context.DonDatSans.Add(don);
            _context.SaveChanges();

            return RedirectToAction("Index", "ThanhToan", new { id = don.MaDatSan });
        }

        // HỦY ĐẶT SÂN
       

        [HttpPost]
        public IActionResult Huy(int id)
        {
            var don = _context.DonDatSans
                .FirstOrDefault(x => x.MaDatSan == id);

            if (don == null)
                return NotFound();

            don.TrangThaiDon = "Đã hủy";

            _context.SaveChanges();

            TempData["Success"] = "Đã hủy lịch đặt.";

            return RedirectToAction(
                "LichSu",
                "ThanhToan");
        }

       
       
    

        [HttpGet]
        public IActionResult DoiLich(int id)
        {
            var don = _context.DonDatSans

                .Include(x => x.MaSanNavigation)

                .Include(x => x.MaGiaNavigation)

                .FirstOrDefault(x => x.MaDatSan == id);

            if (don == null)
                return NotFound();

            ViewBag.BangGia = _context.BangGia

                .Where(x => x.MaSan == don.MaSan)

                .OrderBy(x => x.GioBatDau)

                .ToList();

            return View(don);
        }

        [HttpPost]
        public IActionResult DoiLich(
            int maDatSan,
            int maGia,
            DateOnly ngay)
        {
            var don = _context.DonDatSans
                .FirstOrDefault(x => x.MaDatSan == maDatSan);

            if (don == null)
                return NotFound();

            var bangGia = _context.BangGia
                .FirstOrDefault(x => x.MaGia == maGia);

            if (bangGia == null)
                return NotFound();

            bool trung = _context.DonDatSans.Any(x =>

                x.MaDatSan != maDatSan

                &&

                x.MaSan == don.MaSan

                &&

                x.NgayChoi == ngay

                &&

                x.GioBatDau == bangGia.GioBatDau

                &&

                x.GioKetThuc == bangGia.GioKetThuc

                &&

                x.TrangThaiDon != "Đã hủy");

            if (trung)
            {
                TempData["Error"] = "Khung giờ đã có người đặt.";

                return RedirectToAction(
                    "DoiLich",
                    new
                    {
                        id = maDatSan
                    });
            }

            don.MaGia = maGia;

            don.NgayChoi = ngay;

            don.GioBatDau = bangGia.GioBatDau;

            don.GioKetThuc = bangGia.GioKetThuc;

            _context.SaveChanges();

            TempData["Success"] = "Đổi lịch thành công.";

            return RedirectToAction(
                "LichSu",
                "ThanhToan");
        }
       
    }
}