using System.ComponentModel.DataAnnotations;

namespace ASM2_HA.Models
{
    public class User
    {

        public Guid Id { get; set; } // ID người dùng

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(450, MinimumLength = 5, ErrorMessage = "Tên phải có độ dài từ 5 đến 450 ký tự")]
        public string Name { get; set; } // Họ tên đầy đủ

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên đăng nhập phải từ 5 đến 100 ký tự")]
        public string UserName { get; set; } // Tên đăng nhập

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 100 ký tự")]
        public string Password { get; set; } // Mật khẩu (khuyến nghị mã hóa trước khi lưu)

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public string Role { get; set; } // Vai trò: "Admin" hoặc "Customer"

        public virtual GioHang? GioHang { get; set; } // Liên kết tới giỏ hàng

        public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>(); // Danh sách hóa đơn liên kết
    }
}
