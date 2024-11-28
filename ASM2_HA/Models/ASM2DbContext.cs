using Microsoft.EntityFrameworkCore;

namespace ASM2_HA.Models
{
    public class ASM2DbContext:DbContext
    {
        public ASM2DbContext()
        {
            
        }

        public ASM2DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<GHCT> GHCTs { get; set; }
        public DbSet<HDCT> HDCTs { get; set; }
    }
}
