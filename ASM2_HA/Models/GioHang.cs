using System.ComponentModel.DataAnnotations;

namespace ASM2_HA.Models
{
    public class GioHang
    {
       
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public User? User { get; set; }
        public Guid? UserId { get; set; }   
        public List<GHCT> GHCTs { get; set; }
    }
}
