using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class GioHangConfig : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(x => x.User)
                .WithOne(x => x.GioHang)
                .HasForeignKey<GioHang>(x => x.UserId);


        }
    }
}
