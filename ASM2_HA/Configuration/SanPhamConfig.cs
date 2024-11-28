using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(x => x.ImgUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(x => x.Status)
                 .HasConversion<int>() // Lưu enum dưới dạng số
                 .IsRequired();
        }
    }
}
