using System.ComponentModel.DataAnnotations;

namespace ASM2_HA.Models
{
    public class HoaDon
    {
        
        public Guid Id { get; set; }
        public DateTime? SoldDate { get; set; }
        public decimal? TotalMoney { get; set; }
        public string? Status { get; set; }

        public User? User { get; set; }
        public Guid? UserId { get; set; }
        public List<HDCT> HDCTs { get; set; }
    }
}
