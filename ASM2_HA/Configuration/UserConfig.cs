using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("MatKhau")
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasDefaultValue("KhachHang"); // Mặc định là khách hàng

            builder.HasOne(x => x.GioHang)
                .WithOne(x => x.User)
                .HasForeignKey<GioHang>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa user sẽ xóa luôn giỏ hàng
        }
    }
}
