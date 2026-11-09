using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Thư viện để dùng Session
using web_dat_san_bong_2.Models;
using System.Linq;

namespace web_dat_san_bong_2.Controllers
{
    public class AuthController : Controller
    {
       
        private readonly QuanLyDatSanContext _context;

       
        public AuthController(QuanLyDatSanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (!ModelState.IsValid) return View(model);

            
            var admin = _context.Admins.FirstOrDefault(a => a.TaiKhoan.Trim() == model.Phone.Trim());
            if (admin != null && admin.MatKhau.Trim() == model.Password.Trim())
            {
                HttpContext.Session.SetString("AdminId", admin.MaAD);
                HttpContext.Session.SetString("FullName", admin.TenAD);
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Admin"); 
            }


            var user = _context.NguoiDungs.FirstOrDefault(x => x.SoDienThoai == model.Phone);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.MatKhau))
            {
                // Đăng nhập thành công
                HttpContext.Session.SetInt32("UserId", user.MaNguoiDung);
                HttpContext.Session.SetString("FullName", user.HoTen);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không chính xác.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                ViewBag.Loi = "Dữ liệu nhập chưa hợp lệ (Ví dụ: Mật khẩu chưa đủ 6 ký tự, sai định dạng Email...)";
                return View(model);
            }

            
            if (_context.NguoiDungs.Any(u => u.SoDienThoai == model.Phone))
            {
                ViewBag.Loi = "Số điện thoại này đã có người đăng ký rồi!";
                return View(model);
            }

          
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var newUser = new NguoiDung
            {
                SoDienThoai = model.Phone,
                MatKhau = passwordHash,
                Email = model.Email,
                HoTen = model.FullName,
                TrangThai = "Hoạt động"
            };

            try
            {
                _context.NguoiDungs.Add(newUser);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Đăng ký thành công! Hãy đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
               
                var thongBaoLoi = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ViewBag.Loi = "LỖI DATABASE: " + thongBaoLoi;
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login", "Auth");
        }
        
        
       
    }
}