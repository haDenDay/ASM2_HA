using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_HA.Models
{
    public class HDCT
    {
        public Guid Id { get; set; }
        [ForeignKey("SanPham")]
        public Guid? SanPhamId { get; set; }
        public Guid? HoaDonId { get; set; } 
        public decimal? ToTalMoney { get;set; }
        public SanPham? SanPham { get; set; }
        public HoaDon? HoaDon { get; set; }
       
    }
}
