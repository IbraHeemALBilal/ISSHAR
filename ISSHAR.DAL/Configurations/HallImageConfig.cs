using ISSHAR.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ISSHAR.DAL.Configurations
{
        public class HallImageConfig : IEntityTypeConfiguration<HallImage>
        {
            public void Configure(EntityTypeBuilder<HallImage> builder)
            {
                builder.HasKey(hi => hi.HallImageId);
                builder.Property(hi => hi.HallImageId).IsRequired().ValueGeneratedOnAdd();
                builder.Property(hi => hi.ImageUrl).IsRequired().HasMaxLength(255);
                builder.Property(hi => hi.ImageUrl).IsRequired();
            }
        }
}
