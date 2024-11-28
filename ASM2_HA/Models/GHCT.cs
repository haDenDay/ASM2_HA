using System.ComponentModel.DataAnnotations;

namespace ASM2_HA.Models
{
    public class GHCT
    {
        
        public Guid Id { get; set; }
        public int? SanPhamId { get; set; }
        public Guid? GioHangId { get; set; }    
        public GioHang? GioHang { get; set; }
        public SanPham? SanPham { get; set; }
        public int? SoLuong {  get; set; }
       
    }
}
