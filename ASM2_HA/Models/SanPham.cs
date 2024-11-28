using System.ComponentModel.DataAnnotations;

namespace ASM2_HA.Models
{
    public class SanPham
    {
        
        public Guid Id { get; set; } // ID sản phẩm

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự")]
        public string Name { get; set; } // Tên sản phẩm
        public string? ImgUrl { get; set; } // Đường dẫn ảnh

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        public decimal Price { get; set; } // Giá sản phẩm

        public string? Description { get; set; } // Mô tả sản phẩm

       
        public SanPhamStatus Status { get; set; } = SanPhamStatus.ConHang; // Trạng thái sản phẩm

        public virtual List<GHCT> GHCTs { get; set; } = new List<GHCT>(); // Liên kết giỏ hàng chi tiết
        public virtual List<HDCT> HDCTs { get; set; } = new List<HDCT>(); // Liên kết hóa đơn chi tiết
    }

    public enum SanPhamStatus
    {
        HetHang = 0, // Hết hàng
        ConHang = 1  // Còn hàng
    }
}
