using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_dat_san_bong_2.Models;

namespace web_dat_san_bong_2.Controllers
{
    public class SanBongController : Controller
    {
        private readonly QuanLyDatSanContext _context;

        public SanBongController(QuanLyDatSanContext context)
        {
            _context = context;
        }

       
        // DANH SÁCH SÂN
      
        public IActionResult Index(string? keyword, string? loaiSan)
        {
            // Lưu ý: Nếu báo lỗi Include thì bạn xóa dòng .Include() đi nhé
            var ds = _context.SanTheThaos
                .Include(x => x.BangGia)
                .Where(x => x.TrangThai == "Hoạt động")
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                ds = ds.Where(x => x.TenSan.Contains(keyword) || x.DiaChi.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(loaiSan))
            {
                ds = ds.Where(x => x.LoaiSan == loaiSan);
            }

            ViewBag.Keyword = keyword;
            ViewBag.LoaiSan = loaiSan;

            return View(ds.ToList());
        }

        
        // CHI TIẾT
       
        public IActionResult Detail(int id)
        {
            var san = _context.SanTheThaos
                .Include(x => x.BangGia)
                .Include(x => x.TienIches)
                .FirstOrDefault(x => x.MaSan == id);

            if (san == null)
                return NotFound();

            return View(san);
        }

        
        // XEM LỊCH ĐẶT SÂN
        

        
        public IActionResult LichDat(int id, DateOnly? ngay)
        {
            var san = _context.SanTheThaos

                .Include(x => x.BangGia)

                .FirstOrDefault(x => x.MaSan == id);

            if (san == null)
                return NotFound();

            if (ngay == null)
                ngay = DateOnly.FromDateTime(DateTime.Today);

            
            // Lấy các bảng giá
           

            var bangGia = _context.BangGia

                .Where(x => x.MaSan == id)

                .OrderBy(x => x.GioBatDau)

                .ToList();

          
            // Lấy các đơn đã đặt
         

            var dsDat = _context.DonDatSans

                .Where(x =>

                    x.MaSan == id

                    &&

                    x.NgayChoi == ngay

                    &&

                    x.TrangThaiDon != "Đã hủy")

                .ToList();

            ViewBag.Ngay = ngay;

            ViewBag.BangGia = bangGia;

            ViewBag.DaDat = dsDat;

            return View(san);
        }
    }
}