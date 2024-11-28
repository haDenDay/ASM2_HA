using ASM2_HA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM2_HA.Controllers
{
    public class GHCTController : Controller
    {
        private readonly ASM2DbContext _context;

        public GHCTController(ASM2DbContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            try
            {
                var acc = HttpContext.Session.GetString("TaiKhoan"); // Lấy UserName từ session
                if (string.IsNullOrEmpty(acc))
                {
                    TempData["ErrorMessage"] = "Vui lòng đăng nhập trước khi xem giỏ hàng.";
                    return RedirectToAction("Login", "User"); // Chuyển đến trang đăng nhập của UserController
                }

                var getUser = _context.Users.FirstOrDefault(x => x.UserName == acc);
                if (getUser == null)
                {
                    TempData["ErrorMessage"] = "Người dùng không tồn tại.";
                    return RedirectToAction("Index", "Home");
                }

                var gioHang = _context.GioHangs.FirstOrDefault(x => x.UserId == getUser.Id);
                if (gioHang == null)
                {
                    TempData["ErrorMessage"] = "Bạn chưa có giỏ hàng.";
                    return RedirectToAction("Index", "Home");
                }

                var ghctData = _context.GHCTs.Where(x => x.GioHangId == gioHang.Id).ToList();

                if (!ghctData.Any())
                {
                    TempData["InfoMessage"] = "Giỏ hàng của bạn đang trống.";
                    return RedirectToAction("Index", "Home");
                }

                return View(ghctData);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

    }

}
