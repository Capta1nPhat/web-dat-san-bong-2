using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using web_dat_san_bong_2.Models;

namespace web_dat_san_bong_2.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly QuanLyDatSanContext _context;

        public AdminController(QuanLyDatSanContext context)
        {
            _context = context;
        }

        

        public IActionResult Index()
        {
            var dsSan = _context.SanTheThaos.ToList();
            return View(dsSan);
        }

       
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Create(SanTheThao s)
        {



            int maxId = _context.SanTheThaos.Any() ? _context.SanTheThaos.Max(x => x.MaSan) : 0;
            s.MaSan = maxId + 1;
            // 3. Thêm vào DB
            _context.SanTheThaos.Add(s);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult Edit(int id) 
        {
            var san = _context.SanTheThaos.Find(id);
            if (san == null) return Content("Không tìm thấy sân có mã: " + id);

            return View(san);
        }

        
        [HttpPost]
        public IActionResult Edit(SanTheThao s)
        {
            var san = _context.SanTheThaos.Find(s.MaSan);
            if (san != null)
            {
                
                san.TenSan = s.TenSan;
                san.DiaChi = s.DiaChi;
                san.LoaiSan = s.LoaiSan;
                san.MoTa = s.MoTa;
                san.TrangThai = s.TrangThai;
                san.GioMo = s.GioMo;
                san.GioDong = s.GioDong;
                san.HinhAnh = s.HinhAnh;

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Xóa Sân
        public IActionResult XoaSan(int id)
        {
            var san = _context.SanTheThaos.Find(id);
            return View(san);
        }

        [HttpPost]
        public IActionResult XoaSan(SanTheThao s)
        {
            var san = _context.SanTheThaos.Find(s.MaSan);
            if (san != null)
            {
                _context.SanTheThaos.Remove(san);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        

       
       
    }
}