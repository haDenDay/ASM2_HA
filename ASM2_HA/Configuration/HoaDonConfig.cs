using ASM2_HA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASM2_HA.Configuration
{
    public class HoaDonConfig : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.HoaDons)
                .HasForeignKey(x => x.UserId);
        }
    }
}
