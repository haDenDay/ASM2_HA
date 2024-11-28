using ASM2_HA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM2_HA.Controllers
{
    public class UserController : Controller
    {
        private readonly ASM2DbContext _context;

        public UserController(ASM2DbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(User user)
        {
            try
            {
                // Kiểm tra nếu user đã tồn tại
                if (_context.Users.Any(x => x.UserName == user.UserName))
                {
                    TempData["ErrorMessage"] = "Tài khoản đã tồn tại.";
                    return View(user);
                }

                // Thêm người dùng mới
                _context.Users.Add(user);
                _context.SaveChanges();

                // Tạo giỏ hàng cho người dùng mới
                GioHang gioHang = new GioHang()
                {
                    UserName = user.UserName,
                    UserId = user.Id, // Giỏ hàng cần liên kết với ID người dùng
                };

                _context.GioHangs.Add(gioHang);
                _context.SaveChanges();

                TempData["Status"] = "Chúc mừng bạn đã tạo tài khoản thành công!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tạo tài khoản: {ex.Message}";
                return View(user);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            // Kiểm tra nếu các trường dữ liệu trống
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập tài khoản và mật khẩu.";
                return View();
            }

            var acc = _context.Users.FirstOrDefault(x => x.UserName == userName && x.Password == passWord);

            if (acc == null)
            {
                TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
            else
            {
                // Lưu thông tin tài khoản vào session
                HttpContext.Session.SetString("TaiKhoan", userName);
                return RedirectToAction("Index", "SanPham");
            }
        }
    }

}
