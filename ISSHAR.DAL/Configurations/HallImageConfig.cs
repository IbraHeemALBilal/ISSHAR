using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Configurations
{
        public class HallImageConfig : IEntityTypeConfiguration<HallImage>
        {
            public void Configure(EntityTypeBuilder<HallImage> builder)
            {
                builder.HasKey(a => a.ImageId);
                builder.Property(a => a.ImageId).ValueGeneratedOnAdd();
                builder.Property(a => a.ImageUrl).IsRequired().HasMaxLength(255);
                builder.Property(a => a.ImageUrl).IsRequired();

                builder.HasOne(a => a.Hall)
                       .WithMany(u => u.HallImages)
                       .HasForeignKey(a => a.HallId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
}
