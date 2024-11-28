using ASM2_HA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM2_HA.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ASM2DbContext _context;

        public GioHangController(ASM2DbContext db)
        {
            _context = db;
        }

        // Xem giỏ hàng
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("TaiKhoan");
            if (string.IsNullOrEmpty(userName))
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Login", "User");
            }

            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            var gioHang = _context.GioHangs
                .Include(x => x.GHCTs)
                    .ThenInclude(gc => gc.SanPham)
                .FirstOrDefault(x => x.UserId == user.Id);

            return View(gioHang?.GHCTs ?? new List<GHCT>());
        }

        // Cập nhật số lượng sản phẩm
        [HttpPost]
        public IActionResult UpdateQuantity(int ghctId, int newQuantity)
        {
            var ghct = _context.GHCTs.Find(ghctId);
            if (ghct != null)
            {
                ghct.SoLuong = newQuantity;
                _context.GHCTs.Update(ghct);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật thành công!";
            }
            return RedirectToAction("Index");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public IActionResult RemoveFromCart(int ghctId)
        {
            var ghct = _context.GHCTs.Find(ghctId);
            if (ghct != null)
            {
                _context.GHCTs.Remove(ghct);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Sản phẩm đã được xóa khỏi giỏ hàng!";
            }
            return RedirectToAction("Index");
        }
    }
}
