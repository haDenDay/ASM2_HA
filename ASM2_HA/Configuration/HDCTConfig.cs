using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class HDCTConfig : IEntityTypeConfiguration<HDCT>
    {
        public void Configure(EntityTypeBuilder<HDCT> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.SanPham)
                .WithMany(x => x.HDCTs)
                .HasForeignKey(x => x.SanPhamId)
                .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa sản phẩm nếu đã có trong HDCT

            builder.HasOne(x => x.HoaDon)
                .WithMany(x => x.HDCTs)
                .HasForeignKey(x => x.HoaDonId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa hóa đơn sẽ xóa các HDCT

            builder.Property(x => x.ToTalMoney)
                .HasColumnType("decimal(18,2)")
                .IsRequired(); // Tổng tiền bắt buộc
        }
    }
}
