using Microsoft.AspNetCore.Mvc;
using web_dat_san_bong_2.Models;
using Microsoft.EntityFrameworkCore; 
namespace web_dat_san_bong_2.Controllers
{
    public class QuanLyController : Controller
    {
        private readonly QuanLyDatSanContext _context;
        public QuanLyController(QuanLyDatSanContext context) { _context = context; }

        public IActionResult LichSuDatSan()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            // Lấy danh sách đơn, bao gồm thông tin sân để hiển thị tên
            var lichSu = _context.DonDatSans
                .Include(d => d.MaSanNavigation) 
                .Where(d => d.MaNguoiDung == userId)
                .OrderByDescending(d => d.NgayDat)
                .ToList();

            return View(lichSu);
        }
        // GET: Hiển thị thông tin
        public IActionResult ThongTinCaNhan()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = _context.NguoiDungs.FirstOrDefault(u => u.MaNguoiDung == userId);
            return View(user);
        }

         
        [HttpPost]
        public IActionResult UpdateProfile(NguoiDung model)
        {
            var user = _context.NguoiDungs.Find(model.MaNguoiDung);
            if (user != null)
            {
                user.HoTen = model.HoTen;
                user.SoDienThoai = model.SoDienThoai;
                _context.SaveChanges();
                TempData["Success"] = "Cập nhật thành công!";
            }
            return RedirectToAction("ThongTinCaNhan");
        }
        public IActionResult TichDiemThanhVien()
        {
            
            var danhSachDiem = _context.DiemTichLuys
                .Include(d => d.MaNguoiDungNavigation) 
                .ToList();

            return View(danhSachDiem);
        }
    }
}
