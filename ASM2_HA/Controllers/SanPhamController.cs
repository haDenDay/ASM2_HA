using ASM2_HA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM2_HA.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ASM2DbContext _context;
        public SanPhamController(ASM2DbContext db)
        {
            _context = db;
        }

        //public IActionResult Index(string name,decimal? to ,decimal? from)
        //{
        //    var SanPhamItem = from b in _context.SanPhams select b;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        if(to != null && from != null)
        //        {
        //            SanPhamItem = SanPhamItem.Where(x => x.Name.Contains(name) && x.Price >= to && x.Price <= from);
        //        }
        //        else
        //        {
        //            SanPhamItem = SanPhamItem.Where(x => x.Name.Contains(name));
        //        }

        //    }
        //    else
        //    {
        //        if (to != null && from != null)
        //        {
        //            SanPhamItem = SanPhamItem.Where(x => x.Name.Contains(name) && x.Price >= to && x.Price <= from);
        //        }
        //    }
        //    return View(SanPhamItem);
        //}


        //Sieeuuu content
        public IActionResult Index(string name, decimal? from, decimal? to, int? SanPhamId)
        {
            //gán giá trị -1 nếu SanPhamId ko có giá trị (để đại diện cho "chọn")
            if (SanPhamId == null)
            {
                SanPhamId = -1;
            }
            //Lấy danh sách sản phẩm và thêm mục "chọn"
            var SPs = _context.SanPhams.ToList();
            SPs.Insert(0, new SanPham {  Name = "-------------Chọn----------" });
            //Tạo SelectList với ViewBag để hiển thị dropdown
            ViewBag.SanPhamId = new SelectList(SPs, "Id", "Name", SanPhamId);


            var SanPhamItem = from b in _context.SanPhams select b;

            if (!string.IsNullOrEmpty(name))
            {
                SanPhamItem = SanPhamItem.Where(x => x.Name.Contains(name));
            }

            if (from != null && to != null)
            {
                SanPhamItem = SanPhamItem.Where(x => x.Price >= from && x.Price <= to);
            }

            

            return View(SanPhamItem);
        }


        public IActionResult Details(int id)
        {
            var item = _context.SanPhams.Find(id);
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp,IFormFile upLoadHinh)
        {
            try
            {
                if(upLoadHinh != null && upLoadHinh.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/Imges", upLoadHinh.FileName);

                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    upLoadHinh.CopyTo(fileStream);

                    sp.ImgUrl = "/Imges/"+upLoadHinh.FileName;
                }

                _context.SanPhams.Add(sp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
           
        }
        public IActionResult Edit(int id)
        {
            var edititem = _context.SanPhams.Find(id);
            return View(edititem);
        }
        [HttpPost]
        public IActionResult Edit(SanPham sp,IFormFile upLoadHinh)
        {
            var edititem = _context.SanPhams.Find(sp.Id);
            if(edititem == null)
            {
                return NotFound();
            }
            edititem.Name = sp.Name;
            edititem.Description = sp.Description;
            edititem.Price = sp.Price;
            if (upLoadHinh != null && upLoadHinh.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/Imges", upLoadHinh.FileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                upLoadHinh.CopyTo(fileStream);

                edititem.ImgUrl = "/Imges/" + upLoadHinh.FileName;
            }
            try
            {
                _context.SanPhams.Update(edititem);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult delete(int id)
        {
            var deleteItem = _context.SanPhams.Find(id);
            _context.SanPhams.Remove(deleteItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //thêm sản phẩm vào giỏ hàng
        //xử lý cộng dồn nếu như bị trùng

        public IActionResult AddToCart(int id, int soLuong)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            var userName = HttpContext.Session.GetString("TaiKhoan");
            if (string.IsNullOrEmpty(userName))
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập trước khi thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Login", "User"); // Chuyển hướng đến trang đăng nhập
            }

            // Lấy thông tin tài khoản từ username
            var acc = _context.Users.FirstOrDefault(x => x.UserName == userName);
            if (acc == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tài khoản tương ứng.";
                return RedirectToAction("Index");
            }

            // Lấy giỏ hàng tương ứng với tài khoản
            var gioHang = _context.GioHangs.FirstOrDefault(x =>x.UserId == acc.Id);
            if (gioHang == null) {
                return Content("gio hang loi");
            }
            //if (gioHang == null)
            //{
            //    // Tạo giỏ hàng mới nếu chưa có
            //    gioHang = new GioHang { UserId = acc.Id };
            //    _context.GioHangs.Add(gioHang);
            //    _context.SaveChanges();
            //}

            // Lấy thông tin sản phẩm
            //var sanPham = _context.SanPhams.Find(id);
            //if (sanPham == null)
            //{
            //    TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
            //    return RedirectToAction("Index");
            //}

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
            var lstGHCT = _context.GHCTs.Where(x => x.GioHangId == gioHang.Id).ToList();

            //xử lí cộng dồn
            bool check = false;
            Guid idGHCT = Guid.NewGuid();
            foreach (var item in lstGHCT)
            {
                if (item.SanPhamId == id) // nếu id trong giỏ hàng trùng vs id của sanpham vừa đc add
                {
                    check = true;
                    idGHCT = item.Id;
                    break;
                }
            }
            //nếu sản phẩm đc add chưa tồn tại trong gỏ hàng
            if (!check)
            {
                //tạo ra 1 ghct với sp đó
                GHCT ghct = new GHCT()
                {
                    SanPhamId = id,
                    GioHangId = gioHang.Id,
                    SoLuong = soLuong,
                };
                _context.GHCTs.Add(ghct);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                var ghctUpdate = _context.GHCTs.FirstOrDefault(x => x.Id == idGHCT);
                ghctUpdate.SoLuong = ghctUpdate.SoLuong + soLuong;
                _context.GHCTs.Update(ghctUpdate);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
        }



    }
}
