using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class GHCTConfig : IEntityTypeConfiguration<GHCT>
    {
        public void Configure(EntityTypeBuilder<GHCT> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.SanPham)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.SanPhamId)
                .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa sản phẩm nếu đã có trong GHCT

            builder.HasOne(x => x.GioHang)
                .WithMany(x => x.GHCTs)
                .HasForeignKey(x => x.GioHangId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa giỏ hàng sẽ xóa GHCT

            builder.Property(x => x.SoLuong)
                .IsRequired()
                .HasDefaultValue(1); // Mặc định số lượng là 1
        }
    }
}
